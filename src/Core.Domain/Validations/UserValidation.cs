using System;
using Core.Domain.Exceptions;
using Core.Domain.Models;

namespace Core.Domain.Validations {
    public class UserValidation {
        public void ValidateToken (string token) {
            if (string.IsNullOrEmpty (token))
                throw new ArgumentException ("token é obrigatório");
        }

        public void ValidateName (string name) {
            if (string.IsNullOrEmpty (name))
                throw new ArgumentException ("name é obrigatório");
        }

        public void ValidateEmail (string email) {
            if (string.IsNullOrEmpty (email))
                throw new ArgumentException ("e-mail é obrigatório");
        }

        public void ValidatePassword (string password) {
            if (string.IsNullOrEmpty (password))
                throw new ArgumentException ("senha é obrigatório");
        }

        public void IsLogged (UserModel userModel) {
            if (userModel == null || userModel.Id == 0)
                throw new AuthException ("e-mail ou senha inválido");
        }
    }
}