using System.Collections.Generic;

namespace Core.Domain.Models {
    public class UserGameByGroupViewModel {
        public string Group { get; set; }

        public List<GameViewModel> Games { get; set; }
    }

    public class GameViewModel {
        public int GameUserId { get; set; }

        public string GameDate { get; set; }

        public string TeamA { get; set; }

        public string TeamB { get; set; }

        public int ScoreTeamA { get; set; }

        public int ScoreTeamB { get; set; }
    }
}