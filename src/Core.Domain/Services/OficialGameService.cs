using System.Collections.Generic;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;
using Core.Domain.Validations;
using Core.WebApi.Models;

namespace Core.Domain.Services {
    public class OficialGameService {

        private readonly IOficialGameRepository _oficialGameRepository;

        private readonly OficialGameValidation _oficialGameValidation;

        private readonly UserPointService _userPointService;

        public OficialGameService (IOficialGameRepository oficialGameRepository, OficialGameValidation oficialGameValidation, UserPointService userPointService) {
            _oficialGameRepository = oficialGameRepository;
            _oficialGameValidation = oficialGameValidation;
            _userPointService = userPointService;
        }

        public List<GameByGroup> List (int userId) {
            var gamesByGroupResponse = _oficialGameRepository.List (userId);
            return gamesByGroupResponse;
        }

        public void UpdateScore (OficialGameSaveRequest oficialGameSaveRequest) {
            //--- validações 
            _oficialGameValidation.ValidadeScore (oficialGameSaveRequest.ScoreTeamA);
            _oficialGameValidation.ValidadeScore (oficialGameSaveRequest.ScoreTeamB);
            _oficialGameValidation.CanUpdateScore (oficialGameSaveRequest.UserId);

            //--- atualiza o resultado
            _oficialGameRepository.UpdateScore (oficialGameSaveRequest);

            //--- calcula os pontos dos palpites dos usuarios
            _userPointService.Calculate (oficialGameSaveRequest);
        }

        public void DeleteScore (OficialGameSaveRequest oficialGameSaveRequest) {
            //--- validações 
            _oficialGameValidation.CanUpdateScore (oficialGameSaveRequest.UserId);

            //--- atualiza o resultado
            _oficialGameRepository.DeleteScore (oficialGameSaveRequest.OficialGameId);

            //--- remove o calculo do jogo com o placar deletado
            _userPointService.DeleteByOficialGameId (oficialGameSaveRequest.OficialGameId);
        }
    }
}