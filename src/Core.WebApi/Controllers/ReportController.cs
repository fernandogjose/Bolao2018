using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Models;
using Core.Domain.Services;
using Core.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core.WebApi.Controllers {

    [Route ("api/report")]
    public class ReportController : Controller {
        private readonly ReportService _reportService;

        public ReportController (ReportService reportService) {
            _reportService = reportService;
        }

        [HttpGet ("bet")]
        public List<ReportBet> ListBet () {
            var response = _reportService.ListBet ();
            return response;
        }

        [HttpGet ("bet-by-game/{oficialGameId:int}")]
        public ReportBetDetailByType ListBetByGame (int oficialGameId) {
            var response = _reportService.ListBetByGame (oficialGameId);
            return response;
        }
    }
}