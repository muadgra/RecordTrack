﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecordTrack.Application.Abstractions.Token;
using RecordTrack.Application.Features.Commands.AppUser.CreateUser;
using RecordTrack.Application.Features.Commands.AppUser.GoogleLoginUser;
using RecordTrack.Application.Features.Commands.AppUser.LoginUser;

namespace RecordTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommandRequest request)
        {
            CreateUserCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        
    }
}
