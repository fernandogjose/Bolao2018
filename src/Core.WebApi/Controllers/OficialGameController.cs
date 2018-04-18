using System.Collections.Generic;
using Core.Domain.Models;
using Core.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.WebApi.Controllers
{
    [Route ("api/oficialgame")]
    public class OficialGameController : Controller {
        private readonly OficialGameService _oficialGameService;

        public OficialGameController (OficialGameService oficialGameService) {
            _oficialGameService = oficialGameService;
        }

        [HttpGet]
        public List<GameByGroup> List () {
            var response = _oficialGameService.List();
            return response;
        }
    }
}