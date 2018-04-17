using System;
using System.Collections.Generic;
using Core.Domain.Exceptions;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;
using Core.Domain.Validations;
using Microsoft.Extensions.Caching.Memory;

namespace Core.Domain.Services {
    public class UserGameService {

        private readonly IUserGameRepository _userGameRepository;

        public UserGameService (IUserGameRepository userGameRepository) {
            _userGameRepository = userGameRepository;
        }

        public List<UserGameByGroupViewModel> ListByUserId (int id) {
            var response = _userGameRepository.ListByUserId (id);
            return response;
        }
    }
}