using System.Collections.Generic;
using Core.Domain.Models;
using Core.WebApi.Models;

namespace Core.Domain.Interfaces.Repositories {
    public interface IUserPointRepository {

        void Create (List<UserPoint> userPointsRequest);

        void DeleteByOficialGameId (int oficialGameId);
    }
}