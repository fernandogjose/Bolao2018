using System;

namespace Core.Domain.Exceptions
{
    public class PermissionException : Exception
    {
        public PermissionException(string message)
            : base(message)
        {

        }
    }
}