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

        public void CanSave (UserGameSaveRequest userGameSaveRequest) {
            var userOficialGame = _oficialGameRepository.Get(userGameSaveRequest.OficialGameId);

            if (userOficialGame == null || userOficialGame.Date.AddHours (-4) > DateTime.Now) {
                throw new Exception ("Não é permitido alterar o resultado deste jogo");
            }
        }
    }
}