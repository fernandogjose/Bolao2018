using System;

namespace Core.Domain.Models
{
    public class GameOfGroup
    {
        public int OficialGameId { get; set; }

        public DateTime GameDate { get; set; }

        public string TeamA { get; set; }

        public string TeamB { get; set; }

        public string GroupName { get; set; }

        public int ScoreTeamA { get; set; }

        public int ScoreTeamB { get; set; }

        public bool CanSave { get; set; }

        public bool IsCreateScore { get; set; }
    }
}