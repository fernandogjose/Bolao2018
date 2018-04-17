using System;
using Core.Domain.Exceptions;

namespace Core.Domain.Models
{
    public class OficialGame
    {
        public int Id { get; set; }

        public int GroupId { get; set; }

        public int TeamAId { get; set; }

        public int TeamBId { get; set; }

        public int ScoreTeamA { get; set; }

        public int ScoreTeamB { get; set; }

        public DateTime Date { get; set; }

        public Group Group { get; set; }

        public Team TeamA { get; set; }

        public Team TeamB { get; set; }
    }
}