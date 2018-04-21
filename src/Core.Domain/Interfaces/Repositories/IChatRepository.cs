using System.Collections.Generic;
using Core.Domain.Models;

namespace Core.Domain.Interfaces.Repositories {
    public interface IChatRepository {
        void Create (ChatCreateRequest chatCreateRequest);

        List<Chat> List();
    }
}