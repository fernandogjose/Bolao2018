using System;
using Core.Domain.Exceptions;

namespace Core.Domain.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }
    }
}