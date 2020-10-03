using System;

namespace ArcaneStars.Infrastructure.Exceptions
{
    public class AppServiceException : Exception
    {
        public AppServiceException() { }

        public AppServiceException(string message) : base(message)
        { }

        public AppServiceException(string message, Exception inner) : base(message, inner)
        { }
    }
}
