using System;

namespace Core.Domain.Exceptions
{
    public class DuplicateException : Exception
    {
        public DuplicateException(string message)
            : base(message)
        {

        }
    }
}