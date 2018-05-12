using System.Collections.Generic;

namespace Core.Domain.Models {

    public class GameByGroup {

        public string GroupName { get; set; }

        public List<GameOfGroup> Games { get; set; }
    }
}