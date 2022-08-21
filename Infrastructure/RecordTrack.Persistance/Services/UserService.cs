using Microsoft.AspNetCore.Identity;
using RecordTrack.Application.Abstractions.DTOs.User;
using RecordTrack.Application.Abstractions.Services;
using RecordTrack.Application.Exceptions;
using RecordTrack.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Persistance.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
            }, model.Password);

            CreateUserResponse response = new()
            {
                Success = result.Succeeded
            };

            return response;

        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int refreshTokenLifeTime)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddMinutes(refreshTokenLifeTime);
                await _userManager.UpdateAsync(user);
            }
            else throw new UserNotFoundException();
        }
    }
}
