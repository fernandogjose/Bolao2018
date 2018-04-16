using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Models;
using Core.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.WebApi.Controllers {

    [Route ("api/usergame")]
    public class UserGameController : Controller {
        private readonly UserGameService _userGameService;

        public UserGameController (UserGameService userGameService) {
            _userGameService = userGameService;
        }

        [HttpGet("listbyuserid/{userId:int}")]
        public List<UserGameModel> ListByUserId (int userId) {
            var response = _userGameService.ListByUserId (userId);
            return response;
        }
    }
}