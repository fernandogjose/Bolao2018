using System;

namespace Core.Domain.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException(string message)
            : base(message)
        {

        }

        public static void IsValid(bool isValid, string message)
        {
            if (!isValid)
            {
                throw new AuthException(message);
            }
        }
    }
}