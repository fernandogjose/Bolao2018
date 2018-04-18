using System.Collections.Generic;
using Core.Domain.Models;
using Core.WebApi.Models;

namespace Core.Domain.Interfaces.Repositories {
    public interface IUserGameRepository {

        void DeleteByUserIdAndOficialGameId(int userId, int oficialGameId);

        UserGame GetByUserIdAndOficialGameId(int userId, int oficialGameId);

        void Create (UserGameSaveRequest userGameSaveRequest);

        void Update (UserGameSaveRequest userGameSaveRequest);

        List<GameByGroup> ListByUserId(int userId);
    }
}