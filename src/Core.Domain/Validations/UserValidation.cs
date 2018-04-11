using System;

namespace Core.Domain.Validations
{
    public class UserValidation
    {
        public void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("e-mail é obrigatório");
        }

        public void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("senha é obrigatório");
        }
    }
}