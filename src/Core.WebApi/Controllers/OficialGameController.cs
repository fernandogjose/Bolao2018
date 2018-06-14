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

        private int GetUserId () {
            StringValues userId;
            HttpContext.Request.Headers.TryGetValue ("userId", out userId);
            return Convert.ToInt32 (userId[0]);
        }

        public OficialGameController (OficialGameService oficialGameService) {
            _oficialGameService = oficialGameService;
        }

        [HttpGet]
        public List<GameByGroup> List () {
            int userId = GetUserId ();
            var gamesByGroupResponse = _oficialGameService.List (userId);
            return gamesByGroupResponse;
        }

        [HttpPost]
        public void UpdateScore ([FromBody] OficialGameSaveRequest oficialGameSaveRequest) {
            oficialGameSaveRequest.UserId = GetUserId ();
            _oficialGameService.UpdateScore (oficialGameSaveRequest);
        }

        [HttpDelete ("{oficialGameId}")]
        public void DeleteScore (int oficialGameId) {
            OficialGameSaveRequest oficialGameSaveRequest = new OficialGameSaveRequest ();
            oficialGameSaveRequest.UserId = GetUserId ();
            oficialGameSaveRequest.OficialGameId = oficialGameId;
            _oficialGameService.DeleteScore (oficialGameSaveRequest);
        }
    }
}