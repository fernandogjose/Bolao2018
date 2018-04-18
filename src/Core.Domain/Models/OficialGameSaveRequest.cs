namespace Core.WebApi.Models {
    public class OficialGameSaveRequest {

        public int UserId { get; set; }

        public int OficialGameId { get; set; }

        public int ScoreTeamA { get; set; }

        public int ScoreTeamB { get; set; }
    }
}