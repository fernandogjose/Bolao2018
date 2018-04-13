using System.Collections.Generic;
using Core.Domain.Models;

namespace Core.Domain.Interfaces.Repositories {
    public interface IUserGameRepository {
        UserGameModel Create (UserGameModel request);

        UserGameModel Update (UserGameModel request);

        UserGameModel Get (UserGameModel request);

        List<UserGameModel> List();

        List<UserGameModel> ListByUser(UserGameModel request);
    }
}