using System;
using Core.Domain.Exceptions;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;
using Core.Domain.Validations;
using Microsoft.Extensions.Caching.Memory;

namespace Core.Domain.Services {
    public class UserService {

        private readonly UserValidation _userValidation;

        private readonly IUserRepository _userRepository;

        private readonly IMemoryCache _memoryCache;

        private string CreateToken () {
            var response = Guid.NewGuid () + DateTime.Now.ToString ("yyyyMMddHHmmssFFF");
            return response;
        }

        public UserService (UserValidation userValidation, IUserRepository userRepository, IMemoryCache memoryCache) {
            _userValidation = userValidation;
            _userRepository = userRepository;
            _memoryCache = memoryCache;
        }

        public User Login (string email, string password) {
            _userValidation.ValidateEmail (email);
            _userValidation.ValidatePassword (password);

            User response = _userRepository.Login (email, password);

            _userValidation.IsLogged (response);

            response.Token = CreateToken ();

            _memoryCache.Set<User> ($"userId-{response.Id}", response);

            return response;
        }

        public User GetById (int id) {
            var response = _userRepository.GetById (id);
            return response;
        }

        public User Create (User request) {
            _userValidation.ValidateName (request.Name);
            _userValidation.ValidateEmail (request.Email);
            _userValidation.ValidateDuplicateEmail (request.Email);
            _userValidation.ValidatePassword (request.Password);

            var response = _userRepository.Create (request);
            return response;
        }

        public User Update (User request) {
            _userValidation.ValidateName (request.Name);
            _userValidation.ValidateEmail (request.Email);
            _userValidation.ValidatePassword (request.Password);
            var response = _userRepository.Update (request);
            return response;
        }
    }
}