using System.Collections.Generic;

namespace Core.Domain.Models {

    public class UserGameByGroup {
        public string GroupName { get; set; }

        public List<UserGameOfGroup> UserGames { get; set; }
    }
}