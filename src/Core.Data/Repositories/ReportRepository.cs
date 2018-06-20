using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Core.Data.Sql;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;

namespace Core.Data.Repositories {
    public class ReportRepository : BaseRepository, IReportRepository {
        private readonly ReportSql _reportSql;

        public ReportRepository (ReportSql reportSql) {
            _reportSql = reportSql;
        }

        public List<ReportBet> ListBet () {
            var response = new List<ReportBet> ();

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _reportSql.SqlListBet ();
                    cmd.CommandType = CommandType.Text;

                    conn.Open ();
                    using (DbDataReader dr = cmd.ExecuteReader ()) {
                        while (dr.Read ()) {
                            ReportBet reportBet = new ReportBet ();
                            reportBet.OficialGameId = Convert.ToInt32 (dr["OficialGameId"].ToString ());
                            reportBet.WinTeamA = Convert.ToInt32 (dr["WinTeamA"].ToString ());
                            reportBet.WinTeamB = Convert.ToInt32 (dr["WinTeamB"].ToString ());
                            reportBet.Draw = Convert.ToInt32 (dr["Draw"].ToString ());
                            reportBet.GroupName = dr["GroupName"].ToString ();
                            reportBet.TeamAName = dr["TeamAName"].ToString ();
                            reportBet.TeamBName = dr["TeamBName"].ToString ();
                            reportBet.OficialGameDate = Convert.ToDateTime (dr["OficialGameDate"].ToString ());

                            response.Add (reportBet);
                        }
                    }
                }
            }

            return response;
        }

        public ReportBetDetailByType ListBetByGame (int OficialGameId) {
            ReportBetDetailByType reportBetDetailByTypeResponse = new ReportBetDetailByType ();
            reportBetDetailByTypeResponse.WinsTeamA = new List<ReportBetDetail> ();
            reportBetDetailByTypeResponse.WinsTeamB = new List<ReportBetDetail> ();
            reportBetDetailByTypeResponse.Draws = new List<ReportBetDetail> ();
            List<ReportBetDetail> reportBetDetails = new List<ReportBetDetail> ();

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _reportSql.SqlListBetDetail ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@OficialGameId", GetDbValue (OficialGameId));

                    conn.Open ();
                    using (DbDataReader dr = cmd.ExecuteReader ()) {
                        while (dr.Read ()) {
                            ReportBetDetail reportBetDetail = new ReportBetDetail ();
                            reportBetDetailByTypeResponse.OficialGameId = Convert.ToInt32 (dr["OficialGameId"].ToString ());
                            reportBetDetailByTypeResponse.GroupName = dr["GroupName"].ToString ();
                            reportBetDetailByTypeResponse.TeamAName = dr["TeamAName"].ToString ();
                            reportBetDetailByTypeResponse.TeamBName = dr["TeamBName"].ToString ();
                            reportBetDetailByTypeResponse.OficialGameDate = Convert.ToDateTime (dr["OficialGameDate"].ToString ());
                            reportBetDetail.ScoreTeamA = Convert.ToInt32 (dr["ScoreTeamA"].ToString ());
                            reportBetDetail.ScoreTeamB = Convert.ToInt32 (dr["ScoreTeamB"].ToString ());
                            reportBetDetail.UserName = dr["UserName"].ToString ();
                            reportBetDetails.Add (reportBetDetail);
                        }
                    }
                }
            }

            foreach (var reportBetDetail in reportBetDetails.Where (x => x.ScoreTeamA > x.ScoreTeamB).OrderBy (x => x.UserName)) {
                reportBetDetailByTypeResponse.WinsTeamA.Add (reportBetDetail);
            }

            foreach (var reportBetDetail in reportBetDetails.Where (x => x.ScoreTeamA < x.ScoreTeamB).OrderBy (x => x.UserName)) {
                reportBetDetailByTypeResponse.WinsTeamB.Add (reportBetDetail);
            }

            foreach (var reportBetDetail in reportBetDetails.Where (x => x.ScoreTeamA == x.ScoreTeamB).OrderBy (x => x.UserName)) {
                reportBetDetailByTypeResponse.Draws.Add (reportBetDetail);
            }

            return reportBetDetailByTypeResponse;
        }
    }
}