using System;
using System.Collections.Generic;
using Core.Domain.Exceptions;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;
using Core.Domain.Validations;
using Core.WebApi.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Core.Domain.Services {
    public class UserGameService {

        private readonly IUserGameRepository _userGameRepository;

        private readonly UserGameValidation _userGameValidation;

        public UserGameService (IUserGameRepository userGameRepository, UserGameValidation userGameValidation) {
            _userGameRepository = userGameRepository;
            _userGameValidation = userGameValidation;
        }

        public List<UserGameByGroup> ListByUserId (int id) {
            var response = _userGameRepository.ListByUserId (id);
            return response;
        }

        public UserGame GetByUserIdAndOficialGameId (int userId, int oficialGameId) {
            var userGameResponse = _userGameRepository.GetByUserIdAndOficialGameId (userId, oficialGameId);
            return userGameResponse;
        }

        public void Save (UserGameSaveRequest userGameSaveRequest) {
            var userGameExist = GetByUserIdAndOficialGameId (userGameSaveRequest.UserId, userGameSaveRequest.OficialGameId);
            _userGameValidation.CanSave (userGameExist);

            if (userGameExist != null && userGameExist.ScoreTeamA >= 0 && userGameExist.ScoreTeamB >= 0) {
                _userGameRepository.Update (userGameSaveRequest);
            } else {
                _userGameRepository.Create (userGameSaveRequest);
            }
        }
    }
}