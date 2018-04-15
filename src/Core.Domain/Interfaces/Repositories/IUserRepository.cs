using Core.Domain.Models;

namespace Core.Domain.Interfaces.Repositories {
    public interface IUserRepository {
        UserModel Create (UserModel request);

        UserModel Update (UserModel request);

        UserModel Login (string email, string password);

        UserModel GetById (int id);

        UserModel GetByEmail (string email);
    }
}