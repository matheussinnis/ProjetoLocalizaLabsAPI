using System;

namespace Domain.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string? message = null) : base(message)
        {
        }
    }
}
