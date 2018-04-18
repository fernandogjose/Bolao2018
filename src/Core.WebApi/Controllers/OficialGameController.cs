using System;
using System.Collections.Generic;
using Core.Domain.Models;
using Core.Domain.Services;
using Core.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Core.WebApi.Controllers {
    [Route ("api/oficialgame")]
    public class OficialGameController : Controller {
        private readonly OficialGameService _oficialGameService;

        public OficialGameController (OficialGameService oficialGameService) {
            _oficialGameService = oficialGameService;
        }

        [HttpGet]
        public List<GameByGroup> List () {
            var response = _oficialGameService.List ();
            return response;
        }

        [HttpPut]
        public void UpdateScore (OficialGameSaveRequest oficialGameSaveRequest) {

            StringValues userIdRequest;
            if (!HttpContext.Request.Headers.TryGetValue ("userId", out userIdRequest)) {
                HttpContext.Response.StatusCode = 400;
                HttpContext.Response.WriteAsync ("UserId não encontrado");
                return;
            }

            oficialGameSaveRequest.UserId = Convert.ToInt32(userIdRequest[0]);
            _oficialGameService.UpdateScore (oficialGameSaveRequest);
        }
    }
}