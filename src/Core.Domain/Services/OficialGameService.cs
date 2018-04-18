using System.Collections.Generic;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;
using Core.Domain.Validations;
using Core.WebApi.Models;

namespace Core.Domain.Services {
    public class OficialGameService {

        private readonly IOficialGameRepository _oficialGameRepository;

        private readonly OficialGameValidation _oficialGameValidation;

        public OficialGameService (IOficialGameRepository oficialGameRepository, OficialGameValidation oficialGameValidation) {
            _oficialGameRepository = oficialGameRepository;
            _oficialGameValidation = oficialGameValidation;
        }

        public List<GameByGroup> List () {
            var response = _oficialGameRepository.List ();
            return response;
        }

        public void UpdateScore (OficialGameSaveRequest oficialGameSaveRequest) {
            //--- validações 
            _oficialGameValidation.ValidadeScore(oficialGameSaveRequest.ScoreTeamA);
            _oficialGameValidation.ValidadeScore(oficialGameSaveRequest.ScoreTeamB);
            _oficialGameValidation.CanUpdateScore(oficialGameSaveRequest.UserId);

            //--- atualiza o resultado
            _oficialGameRepository.UpdateScore(oficialGameSaveRequest);
        }
    }
}