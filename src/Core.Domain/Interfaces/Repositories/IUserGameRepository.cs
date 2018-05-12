using System.Collections.Generic;
using Core.Domain.Models;
using Core.WebApi.Models;

namespace Core.Domain.Interfaces.Repositories {
    public interface IUserGameRepository {

        void DeleteByUserIdAndOficialGameId(int userId, int oficialGameId);

        UserGame GetByUserIdAndOficialGameId(int userId, int oficialGameId);

        void Save (List<GameByGroup> gamesByGroupRequest);

        List<GameByGroup> ListByUserId(int userId);

        List<UserGameScore> ListByOficialGameId (int id);
    }
}