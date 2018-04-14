using Core.Domain.Models;

namespace Core.Domain.Interfaces.Repositories {
    public interface IUserRepository {
        UserModel Create (UserModel request);

        UserModel Update (UserModel request);

        UserModel Login (UserModel request);

        UserModel Get (UserModel request);
    }
}