using System.Collections.Generic;
using Core.Domain.Models;
using Core.WebApi.Models;

namespace Core.Domain.Interfaces.Repositories {
    public interface IUserGameRepository {

        void DeleteByUserIdAndOficialGameId(int userId, int oficialGameId);

        void Create (UserGameSaveRequest userGameSaveRequest);

        List<UserGameByGroup> ListByUserId(int userId);
    }
}