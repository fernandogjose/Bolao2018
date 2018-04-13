using System;
using Core.Domain.Exceptions;

namespace Core.Domain.Models
{
    public class UserGameModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int GameId { get; set; }

        public int ScoreTeamA { get; set; }

        public int ScoreTeamB { get; set; }

        public int Points { get; set; }

        public UserModel User { get; set; }

        public GameModel Game { get; set; }
    }
}