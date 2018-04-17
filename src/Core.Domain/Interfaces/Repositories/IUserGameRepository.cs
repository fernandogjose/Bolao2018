using System.Collections.Generic;
using Core.Domain.Models;

namespace Core.Domain.Interfaces.Repositories {
    public interface IUserGameRepository {
        UserGameModel Create (UserGameModel request);

        UserGameModel Update (UserGameModel request);

        List<UserGameByGroupViewModel> ListByUserId(int userId);
    }
}