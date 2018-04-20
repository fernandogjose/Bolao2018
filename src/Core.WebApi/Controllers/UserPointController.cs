using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Models;
using Core.Domain.Services;
using Core.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core.WebApi.Controllers {

    [Route ("api/userpoint")]
    public class UserPointController : Controller {
        private readonly UserPointService _userPointService;

        public UserPointController (UserPointService userPointService) {
            _userPointService = userPointService;
        }

        [HttpGet]
        public List<UserPointClassification> List () {
            var response = _userPointService.List ();
            return response;
        }
    }
}