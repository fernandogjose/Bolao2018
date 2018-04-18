using System;
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

            User userResponse = null;

            //--- validação dos parâmetros
            _userValidation.ValidateEmail (email);
            _userValidation.ValidatePassword (password);

            //--- verifica se o usuário esta em cache
            var userCache = _memoryCache.Get<User> ($"userEmail-{email}");
            if (userCache == null) {

                //--- faz o login, verifica se logou
                userResponse = _userRepository.Login (email, password);
                _userValidation.IsLogged (userResponse);

                //--- grava o usuário em cache por email
                _memoryCache.Set<User> ($"userEmail-{userResponse.Email}", userResponse);
            } else {
                userResponse = userCache;
            }

            //--- cria o token para esta sessão
            userResponse.Token = CreateToken ();

            //--- grava o usuário em cache por id para a autenticacao dos usuários nos httprequest
            _memoryCache.Set<User> ($"userId-{userResponse.Id}", userResponse);            

            return userResponse;
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