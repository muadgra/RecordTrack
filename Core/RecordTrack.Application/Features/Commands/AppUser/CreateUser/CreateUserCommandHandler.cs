using MediatR;
using Microsoft.AspNetCore.Identity;
using RecordTrack.Application.Abstractions.DTOs.User;
using RecordTrack.Application.Abstractions.Services;
using RecordTrack.Application.Exceptions;

namespace RecordTrack.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {

            CreateUserResponse response = await _userService.CreateAsync(new()
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
                Username = request.Username
            });
            return new()
            {
                Success = response.Success,
                Message = response.Message
            };
        }
    }
}
