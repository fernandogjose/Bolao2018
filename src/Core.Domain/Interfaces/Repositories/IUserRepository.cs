using Core.Domain.Models;

namespace Core.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
         UserModel Login(UserModel request);
         
         int Create(UserModel request);
    }
}