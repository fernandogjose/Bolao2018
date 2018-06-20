using System;
using Core.Domain.Exceptions;

namespace Core.Domain.Models {
    public class ReportBet {
        public int OficialGameId { get; set; }

        public DateTime OficialGameDate { get; set; }

        public string GroupName { get; set; }

        public string TeamAName { get; set; }

        public string TeamBName { get; set; }

        public int WinTeamA { get; set; }

        public int WinTeamB { get; set; }

        public int Draw { get; set; }
    }
}