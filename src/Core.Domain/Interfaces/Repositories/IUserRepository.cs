using System.Collections.Generic;
using Core.Domain.Models;

namespace Core.Domain.Interfaces.Repositories {
    public interface IUserRepository {
        User Create (User request);

        User Update (User request);

        User Login (string email, string password);

        User GetById (int id);

        User GetByEmail (string email);

        List<User> List();
    }
}