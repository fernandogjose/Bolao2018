using System.Collections.Generic;

namespace Core.Domain.Models {
    public class UserGameByGroupViewModel {
        public string GroupName { get; set; }

        public List<GameViewModel> Games { get; set; }
    }

    public class GameViewModel {
        public int OficialGameId { get; set; }

        public string GameDate { get; set; }

        public string TeamA { get; set; }

        public string TeamB { get; set; }

        public string GroupName { get; set; }

        public int ScoreTeamA { get; set; }

        public int ScoreTeamB { get; set; }
    }
}