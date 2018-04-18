using System;
using Core.Domain.Helpers;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;
using Core.WebApi.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Core.Domain.Validations {
    public class UserGameValidation {
        private readonly IOficialGameRepository _oficialGameRepository;

        public UserGameValidation (IOficialGameRepository oficialGameRepository) {
            _oficialGameRepository = oficialGameRepository;
        }

        public void CanSave (UserGame userGameRequest) {
            if (userGameRequest == null ||
                userGameRequest.OficialGame == null ||
                DateTime.Now > userGameRequest.OficialGame.Date.AddHours (-4)) {
                throw new Exception ("Não é permitido alterar o resultado deste jogo");
            }
        }
    }
}