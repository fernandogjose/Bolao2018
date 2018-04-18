using System.Collections.Generic;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;

namespace Core.Domain.Services
{
    public class OficialGameService {

        private readonly IOficialGameRepository _oficialGameRepository;

        public OficialGameService (IOficialGameRepository oficialGameRepository) {
            _oficialGameRepository = oficialGameRepository;
        }

        public List<GameByGroup> List () {
            var response = _oficialGameRepository.List ();
            return response;
        }
    }
}