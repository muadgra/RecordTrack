using MediatR;
using Microsoft.AspNetCore.Identity;
using RecordTrack.Application.Exceptions;

namespace RecordTrack.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.Username,
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
            }, request.Password);
            if (result.Succeeded)
            {
                return new()
                {
                    Success = true,
                    Message = "User has been created"
                };
            }
            return new()
            {
                Success = false,
                Message = "Error while creating a user"
            };
        }
    }
}
