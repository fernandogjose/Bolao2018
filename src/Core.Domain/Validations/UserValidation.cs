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

        public void ValidateDuplicateEmail (string email) {
            if (_userRepository.GetByEmail (email) != null) {
                throw new DuplicateException ("e-mail já cadastrado");
            }
        }

        public void ValidatePassword (string password) {
            if (string.IsNullOrEmpty (password))
                throw new ArgumentException ("senha é obrigatório");
        }

        public void IsLogged (User user) {
            if (user == null || user.Id == 0)
                throw new AuthException ("e-mail ou senha inválido");
        }
    }
}