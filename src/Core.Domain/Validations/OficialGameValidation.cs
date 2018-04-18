using System;
using Core.Domain.Exceptions;
using Core.Domain.Helpers;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;
using Core.WebApi.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Core.Domain.Validations {
    public class OficialGameValidation {

        public void ValidadeScore (int score) {
            if (score < 0) {
                throw new ArgumentException ("Placar inválido");
            }
        }

        public void CanUpdateScore (int userId) {
            if (userId != 1) {
                throw new PermissionException ("usuário sem permissão para atualizar o placar oficial");
            }
        }
    }
}