﻿using RecordTrack.Application.Abstractions.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Features.Commands.AppUser.GoogleLoginUser
{
    public class GoogleLoginCommandResponse
    {
        public Token Token { get; set; }
    }
}
