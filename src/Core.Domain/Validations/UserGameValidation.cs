using System;
using Core.Domain.Exceptions;
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

        public void CanSave () {
            if (DateTime.Now > new DateTime (2018, 6, 13)) {
                throw new PermissionException ("Não é permitido alterar o resultado deste jogo");
            }
        }
    }
}