using System;
using Core.Domain.Exceptions;

namespace Core.Domain.Models
{
    public class UserGame
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int OficialGameId { get; set; }

        public int ScoreTeamA { get; set; }

        public int ScoreTeamB { get; set; }

        public int Points { get; set; }

        public OficialGame OficialGame { get; set; }
    }
}