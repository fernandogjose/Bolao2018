using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Models;
using Core.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.WebApi.Controllers {

    [Route ("api/user")]
    public class UserController : Controller {
        private readonly UserService _userService;

        public UserController (UserService userService) {
            _userService = userService;
        }

        [HttpGet ("login/{email}/{password}")]
        public UserModel Get (string email, string password) {
            var request = new UserModel {
                Email = email,
                Password = password
            };

            var response = _userService.Login (request);
            return response;
        }

        [HttpGet]
        public UserModel Get (string token) {
            var request = new UserModel {
                Token = token
            };

            var response = _userService.Get (request);
            return response;
        }

        [HttpPost]
        public UserModel Post ([FromBody] UserModel request) {
            var response = _userService.Create (request);
            return response;
        }

        [HttpPut]
        public UserModel Put ([FromBody] UserModel request) {
            var response = _userService.Update (request);
            return response;
        }
    }
}