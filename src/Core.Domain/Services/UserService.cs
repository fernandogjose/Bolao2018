using Core.Domain.Exceptions;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;
using Core.Domain.Validations;

namespace Core.Domain.Services {
    public class UserService {

        private readonly UserValidation _userValidation;

        private readonly IUserRepository _userRepository;

        public UserService (UserValidation userValidation, IUserRepository userRepository) {
            _userValidation = userValidation;
            _userRepository = userRepository;
        }

        public UserModel Login (UserModel request) {

            _userValidation.ValidateEmail (request.Email);
            _userValidation.ValidatePassword (request.Password);
            var response = _userRepository.Login (request);
            AuthException.IsValid (response != null && response.Id > 0, "e-mail ou senha inválido");
            _userRepository.UpdateToken(response);
            return response;
        }

        public UserModel Get (UserModel request) {

            _userValidation.ValidateToken (request.Token);
            var response = _userRepository.Get (request);
            return response;
        }

        public UserModel Create (UserModel request) {
            _userValidation.ValidateName (request.Name);
            _userValidation.ValidateEmail (request.Email);
            _userValidation.ValidatePassword (request.Password);
            var response = _userRepository.Create (request);
            return response;
        }

        public UserModel Update (UserModel request) {
            _userValidation.ValidateName (request.Name);
            _userValidation.ValidateEmail (request.Email);
            _userValidation.ValidatePassword (request.Password);
            var response = _userRepository.Update (request);
            return response;
        }
    }
}