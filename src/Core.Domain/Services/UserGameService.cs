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

        public void Save (UserGameSaveRequest userGameSaveRequest) {
            _userGameValidation.CanSave(userGameSaveRequest);
            _userGameRepository.DeleteByUserIdAndOficialGameId (userGameSaveRequest.UserId, userGameSaveRequest.OficialGameId);
            _userGameRepository.Create (userGameSaveRequest);
        }
    }
}