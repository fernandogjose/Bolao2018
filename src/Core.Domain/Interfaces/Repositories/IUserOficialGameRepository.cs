using System.Collections.Generic;
using Core.Domain.Models;

namespace Core.Domain.Interfaces.Repositories {
    public interface IOficialGameRepository {

        OficialGame Get(int id);

         List<GameByGroup> List ();
    }
}