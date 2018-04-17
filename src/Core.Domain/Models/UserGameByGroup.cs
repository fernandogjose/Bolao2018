using System.Collections.Generic;

namespace Core.Domain.Models {
    
    public class UserGameByGroup {
        public string GroupName { get; set; }

        public List<Game> Games { get; set; }
    }

    public class Game {
        public int OficialGameId { get; set; }

        public string GameDate { get; set; }

        public string TeamA { get; set; }

        public string TeamB { get; set; }

        public string GroupName { get; set; }

        public int ScoreTeamA { get; set; }

        public int ScoreTeamB { get; set; }
    }
}