using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RecordTrack.Application.Abstractions.DTOs;
using RecordTrack.Application.Abstractions.Services;
using RecordTrack.Application.Abstractions.Token;
using RecordTrack.Domain.Entities.Identity;
using RecordTrack.Application.Abstractions.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordTrack.Application.Exceptions;
using Google.Apis.Auth;

namespace RecordTrack.Persistance.Services
{
    public class AuthService : IAuthService
    {
        readonly IConfiguration _configuration;
        readonly HttpClient _httpClient;
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly SignInManager<AppUser> _signInManager;
        readonly IUserService _userService;
        public AuthService(IConfiguration configuration, HttpClient httpClient, UserManager<AppUser> userManager, 
            ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IUserService userService)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
        }
       public async Task<Token> CreateExternalAsync(AppUser user, string email, string name, UserLoginInfo info, int addOnAccessTokenDate)
        {
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName = email,
                        Name = name,
                        Surname = ""
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }

            if (result)
            {
                await _userManager.AddLoginAsync(user, info); //AspNetUserLogins

                Token token = _tokenHandler.CreateAccessToken(addOnAccessTokenDate, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 5);

                
                return token;
            }
            throw new Exception("Invalid external authentication.");
        }


        public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["GoogleAuth:ClientId"] }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
            AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await CreateExternalAsync(user, payload.Email, payload.Name, info, accessTokenLifeTime);
        }



        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                throw new Exception("User not found");

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded) //Authentication başarılı!
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 5);
                return token;
            }
            throw new AuthenticationErrorException();
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = _userManager.Users.FirstOrDefault(x => x.Id == refreshToken);
            if(user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(15, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 15);
                return token;
            }

            throw new UserNotFoundException();
        }
    }
}
