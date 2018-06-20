using System;
using Core.Domain.Exceptions;

namespace Core.Domain.Models {
    public class ReportBetDetail {
       
        public string UserName { get; set; }

        public int ScoreTeamA { get; set; }

        public int ScoreTeamB { get; set; }
    }
}