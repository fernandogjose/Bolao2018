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

        private readonly IUserRepository _userRepository;

        public UserPointRepository (UserPointSql userPointSql, IUserRepository userRepository) {
            _userPointSql = userPointSql;
            _userRepository = userRepository;
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

        public List<UserPointClassification> List () {
            List<UserPointClassification> userPointClassificationsResponse = new List<UserPointClassification> ();

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userPointSql.SqlList ();
                    cmd.CommandType = CommandType.Text;

                    conn.Open ();
                    using (DbDataReader dr = cmd.ExecuteReader ()) {

                        int position = 1;
                        while (dr.Read ()) {

                            UserPointClassification userPointClassification = new UserPointClassification ();
                            userPointClassification.UserName = dr["UserName"].ToString ();
                            userPointClassification.UserId = Convert.ToInt32 (dr["UserId"].ToString ());
                            userPointClassification.VCPC = Convert.ToInt32 (dr["VCPC"].ToString ());
                            userPointClassification.VCUPC = Convert.ToInt32 (dr["VCUPC"].ToString ());
                            userPointClassification.VCPE = Convert.ToInt32 (dr["VCPE"].ToString ());
                            userPointClassification.ECPC = Convert.ToInt32 (dr["ECPC"].ToString ());
                            userPointClassification.ECPE = Convert.ToInt32 (dr["ECPE"].ToString ());
                            userPointClassification.RE = Convert.ToInt32 (dr["RE"].ToString ());
                            userPointClassification.Total = Convert.ToInt32 (dr["Total"].ToString ());
                            userPointClassification.Position = position;
                            userPointClassificationsResponse.Add (userPointClassification);

                            position++;
                        }
                    }
                }
            }

            if (userPointClassificationsResponse.Count () == 0) {
                int position = 1;
                foreach (var user in _userRepository.List ()) {
                    UserPointClassification userPointClassification = new UserPointClassification ();
                    userPointClassification.UserName = user.Name;
                    userPointClassification.UserId = user.Id;
                    userPointClassification.VCPC = 0;
                    userPointClassification.VCUPC = 0;
                    userPointClassification.VCPE = 0;
                    userPointClassification.ECPC = 0;
                    userPointClassification.ECPE = 0;
                    userPointClassification.RE = 0;
                    userPointClassification.Total = 0;
                    userPointClassification.Position = position;
                    userPointClassificationsResponse.Add (userPointClassification);

                    position++;
                }
            }

            return userPointClassificationsResponse;
        }
    }
}