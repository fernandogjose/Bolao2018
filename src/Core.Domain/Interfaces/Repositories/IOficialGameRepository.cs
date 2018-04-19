using System.Collections.Generic;
using Core.Domain.Models;
using Core.WebApi.Models;

namespace Core.Domain.Interfaces.Repositories {
    public interface IOficialGameRepository {

        OficialGame Get (int id);

        List<GameByGroup> List (int userId);

        void UpdateScore(OficialGameSaveRequest oficialGameSaveRequest);

        void DeleteScore(int oficialGameId);
    }
}