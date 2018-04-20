using System;
using Core.Domain.Exceptions;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Core.Domain.Validations {

    public class UserValidation {
        private readonly IUserRepository _userRepository;

        private readonly IMemoryCache _memoryCache;

        public UserValidation (IUserRepository userRepository, IMemoryCache memoryCache) {
            _userRepository = userRepository;
            _memoryCache = memoryCache;
        }

        public bool RequestIsValid (string token, int userId) {
            var userCache = _memoryCache.Get<User> ($"userId-{userId}");
            if (userCache == null)
                return false;
            return userCache.Token == token && userCache.Id == userId;
        }

        public void ValidateToken (string token) {
            if (string.IsNullOrEmpty (token))
                throw new ArgumentException ("Assim não rola mano, o token é obrigatório");
        }

        public void ValidateName (string name) {
            if (string.IsNullOrEmpty (name))
                throw new ArgumentException ("Assim não rola mano, o nome é obrigatório");
        }

        public void ValidateEmail (string email) {
            if (string.IsNullOrEmpty (email))
                throw new ArgumentException ("Assim não rola mano, o e-mail é obrigatório");
        }

        public void ValidateDuplicateEmail (string email) {

            var userPorEmail = _userRepository.GetByEmail (email);
            if (userPorEmail != null && userPorEmail.Id > 0) {
                throw new DuplicateException ("Assim não rola mano, esse e-mail já esta sendo usado por outro boleiro");
            }
        }

        public void ValidatePassword (string password) {
            if (string.IsNullOrEmpty (password))
                throw new ArgumentException ("Assim não rola mano, a senha é obrigatória");
        }

        public void IsLogged (User user, string password) {
            if (user == null || user.Id == 0)
                throw new AuthException ("Xiiiiii parça, esta metendo a mala ai mas não lembra a senha, seu e-mail ou sua senha esta errada");

            if (user.Password != password)
                throw new AuthException ("Xiiiiii parça, esta metendo a mala ai mas não lembra a senha, seu e-mail ou sua senha esta errada");
        }
    }
}