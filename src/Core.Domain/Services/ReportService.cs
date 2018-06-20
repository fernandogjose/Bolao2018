using System.Collections.Generic;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;
using Core.Domain.Validations;
using Core.WebApi.Models;

namespace Core.Domain.Services {
    public class ReportService {

        private readonly IReportRepository _reportRepository;

        public ReportService (IReportRepository reportRepository) {
            _reportRepository = reportRepository;
        }

        public List<ReportBet> ListBet () {
            var response = _reportRepository.ListBet ();
            return response;
        }

        public ReportBetDetailByType ListBetByGame (int oficialGameId) {
            var response = _reportRepository.ListBetByGame (oficialGameId);
            return response;
        }
    }
}