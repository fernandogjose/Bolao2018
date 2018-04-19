using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Core.Data.Sql;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;
using Core.WebApi.Models;

namespace Core.Data.Repositories {
    public class UserPointRepository : BaseRepository, IUserPointRepository {
        private readonly UserPointSql _userPointSql;

        public UserPointRepository (UserPointSql userPointSql) {
            _userPointSql = userPointSql;
        }

        public void Create (List<UserPoint> userPointsRequest) {
            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                conn.Open ();

                foreach (var userPointRequest in userPointsRequest) {
                    using (var cmd = new SqlCommand ()) {
                        cmd.Connection = conn;
                        cmd.CommandText = _userPointSql.SqlCreate ();
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue ("@UserId", GetDbValue (userPointRequest.UserId));
                        cmd.Parameters.AddWithValue ("@OficialGameId", GetDbValue (userPointRequest.OficialGameId));
                        cmd.Parameters.AddWithValue ("@PointTypeId", GetDbValue (Convert.ToInt32 (userPointRequest.PointType)));
                        cmd.ExecuteNonQuery ();
                    }
                }
            }
        }

        public void DeleteByOficialGameId (int oficialGameId) {
            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userPointSql.SqlDeleteByOficialGameId ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@OficialGameId", GetDbValue (oficialGameId));

                    conn.Open ();
                    cmd.ExecuteNonQuery ();
                }
            }
        }

        // public List<UserPoint> List () {
        //     List<UserGameScore> userGameScoresResponse = new List<UserGameScore> ();

        //     using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
        //         using (var cmd = new SqlCommand ()) {
        //             cmd.Connection = conn;
        //             cmd.CommandText = _userPointSql.SqlListByOficialGameId ();
        //             cmd.CommandType = CommandType.Text;
        //             cmd.Parameters.AddWithValue ("@OficialGameId", GetDbValue (oficialGameId));

        //             conn.Open ();
        //             using (DbDataReader dr = cmd.ExecuteReader ()) {
        //                 while (dr.Read ()) {

        //                     UserGameScore userGameScore = new UserGameScore ();
        //                     userGameScore.UserId = Convert.ToInt32 (dr["UserId"].ToString ());
        //                     userGameScore.OficialGameId = oficialGameId;
        //                     userGameScore.ScoreTeamA = Convert.ToInt32 (dr["ScoreTeamA"].ToString ());
        //                     userGameScore.ScoreTeamB = Convert.ToInt32 (dr["ScoreTeamB"].ToString ());

        //                     userGameScoresResponse.Add (userGameScore);
        //                 }
        //             }
        //         }
        //     }

        //     return userGameScoresResponse;
        // }
    }
}