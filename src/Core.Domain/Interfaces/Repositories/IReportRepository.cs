using System.Collections.Generic;
using Core.Domain.Models;

namespace Core.Domain.Interfaces.Repositories {
    public interface IReportRepository {

        List<ReportBet> ListBet();

        ReportBetDetailByType ListBetByGame (int OficialGameId);
    }
}