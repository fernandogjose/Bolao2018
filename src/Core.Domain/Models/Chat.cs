using System;
using Core.Domain.Exceptions;

namespace Core.Domain.Models
{
    public class Chat
    {
        public string UserId { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }
    }
}