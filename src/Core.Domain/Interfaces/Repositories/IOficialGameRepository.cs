using System.Collections.Generic;
using Core.Domain.Models;
using Core.WebApi.Models;

namespace Core.Domain.Interfaces.Repositories {
    public interface IOficialGameRepository {

        OficialGame Get (int id);

        List<GameByGroup> List ();

        void UpdateScore(OficialGameSaveRequest oficialGameSaveRequest);
    }
}