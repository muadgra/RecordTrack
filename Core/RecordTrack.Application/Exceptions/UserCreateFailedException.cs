using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Exceptions
{
    public class UserCreateFailedException : Exception
    {
        public UserCreateFailedException() : base("Failed to create a user")
        {

        }
        public UserCreateFailedException(string? message) : base(message)
        {

        }
        public UserCreateFailedException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
