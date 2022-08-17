using RecordTrack.Application.Abstractions.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
    }

    public class LoginUserCommandSuccessResponse : LoginUserCommandResponse
    {
        public Token Token { get; set; }

    }
    public class LoginUserCommandFailResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }

    }
}
