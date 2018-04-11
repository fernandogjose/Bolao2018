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

            _userValidation.ValidateEmail(request.Email);
            _userValidation.ValidatePassword(request.Password);

            var response = _userRepository.Login (request);

            AuthException.IsValid (response != null && response.Id > 0, "e-mail ou senha inválido");

            return response;
        }
    }
}