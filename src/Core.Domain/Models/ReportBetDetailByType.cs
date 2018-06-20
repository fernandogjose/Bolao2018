using System;
using System.Collections.Generic;
using Core.Domain.Exceptions;

namespace Core.Domain.Models {
    public class ReportBetDetailByType {
        public int OficialGameId { get; set; }

        public DateTime OficialGameDate { get; set; }

        public string GroupName { get; set; }

        public string TeamAName { get; set; }

        public string TeamBName { get; set; }

        public List<ReportBetDetail> WinsTeamA { get; set; }

        public List<ReportBetDetail> WinsTeamB { get; set; }

        public List<ReportBetDetail> Draws { get; set; }
    }
}