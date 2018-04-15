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
            var response = _userService.Login (email, password);
            return response;
        }

        [HttpGet]
        public UserModel Get (int id) {
            var response = _userService.GetById (id);
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