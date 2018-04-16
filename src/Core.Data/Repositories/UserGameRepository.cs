using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Core.Data.Sql;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;

namespace Core.Data.Repositories {
    public class UserGameRepository : BaseRepository, IUserGameRepository {
        private readonly UserGameSql _userGameSql;

        public UserGameRepository (UserGameSql userGameSql) {
            _userGameSql = userGameSql;
        }

        public UserGameModel Create (UserGameModel request) {

            var response = request;

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userGameSql.SqlCreate ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@UserId", GetDbValue (request.UserId));
                    cmd.Parameters.AddWithValue ("@OficialGameId", GetDbValue (request.OficialGameId));
                    cmd.Parameters.AddWithValue ("@ScoreTeamA", GetDbValue (request.ScoreTeamA));
                    cmd.Parameters.AddWithValue ("@ScoreTeamB", GetDbValue (request.ScoreTeamB));
                    cmd.Parameters.AddWithValue ("@Points", GetDbValue (request.Points));
                    conn.Open ();
                    response.Id = Convert.ToInt32 (cmd.ExecuteScalar ());
                }
            }

            return response;
        }

        public UserGameModel Update (UserGameModel request) {

            var response = request;

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userGameSql.SqlUpdate ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@UserId", GetDbValue (request.UserId));
                    cmd.Parameters.AddWithValue ("@OficialGameId", GetDbValue (request.OficialGameId));
                    cmd.Parameters.AddWithValue ("@ScoreTeamA", GetDbValue (request.ScoreTeamA));
                    cmd.Parameters.AddWithValue ("@ScoreTeamB", GetDbValue (request.ScoreTeamB));
                    cmd.Parameters.AddWithValue ("@Points", GetDbValue (request.Points));
                    conn.Open ();
                    cmd.ExecuteNonQuery ();
                }
            }

            return response;
        }

        public UserGameModel Get (UserGameModel request) {
            var response = new UserGameModel ();

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userGameSql.SqlGet ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@Id", GetDbValue (request.Id));

                    conn.Open ();
                    using (DbDataReader dr = cmd.ExecuteReader ()) {
                        if (dr.Read ()) {
                            response.Id = Convert.ToInt32 (dr["Id"].ToString ());
                            response.UserId = Convert.ToInt32 (dr["UserId"].ToString ());
                            response.OficialGameId = Convert.ToInt32 (dr["OficialGameId"].ToString ());
                        }
                    }
                }
            }

            return response;
        }

        public List<UserGameModel> List () {
            var response = new List<UserGameModel> ();

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userGameSql.SqlList ();
                    cmd.CommandType = CommandType.Text;

                    conn.Open ();
                    using (DbDataReader dr = cmd.ExecuteReader ()) {
                        while (dr.Read ()) {
                            var userGame = new UserGameModel ();
                            userGame.Id = Convert.ToInt32 (dr["Id"].ToString ());
                            userGame.UserId = Convert.ToInt32 (dr["UserId"].ToString ());
                            userGame.OficialGameId = Convert.ToInt32 (dr["OficialGameId"].ToString ());
                            userGame.ScoreTeamA = Convert.ToInt32 (dr["ScoreTeamA"].ToString ());
                            userGame.ScoreTeamB = Convert.ToInt32 (dr["ScoreTeamB"].ToString ());
                            userGame.Points = Convert.ToInt32 (dr["Points"].ToString ());

                            userGame.User = new UserModel ();
                            userGame.User.Id = Convert.ToInt32 (dr["UserId"].ToString ());
                            userGame.User.Name = dr["UserName"].ToString ();

                            userGame.OficialGame = new OficialGameModel ();
                            userGame.OficialGame.Id = Convert.ToInt32 (dr["GameId"].ToString ());
                            userGame.OficialGame.GroupId = Convert.ToInt32 (dr["GroupId"].ToString ());
                            userGame.OficialGame.TeamAId = Convert.ToInt32 (dr["TeamAId"].ToString ());
                            userGame.OficialGame.TeamBId = Convert.ToInt32 (dr["TeamBId"].ToString ());

                            userGame.OficialGame.Group = new GroupModel ();
                            userGame.OficialGame.Group.Id = Convert.ToInt32 (dr["GroupId"].ToString ());
                            userGame.OficialGame.Group.Name = dr["GroupName"].ToString ();

                            userGame.OficialGame.TeamA = new TeamModel ();
                            userGame.OficialGame.TeamA.Id = Convert.ToInt32 (dr["TeamAId"].ToString ());
                            userGame.OficialGame.TeamA.Name = dr["TeamAName"].ToString ();

                            userGame.OficialGame.TeamB = new TeamModel ();
                            userGame.OficialGame.TeamB.Id = Convert.ToInt32 (dr["TeamBId"].ToString ());
                            userGame.OficialGame.TeamB.Name = dr["TeamBName"].ToString ();

                            response.Add (userGame);
                        }
                    }
                }
            }

            return response;
        }

        public List<UserGameByGroupViewModel> ListByUserId (int userId) {
            var response = new List<UserGameByGroupViewModel> ();

            var groupIdAux = 0;
            for (int i = 1; i < 20; i++) {
                UserGameByGroupViewModel userGameByGroup = new UserGameByGroupViewModel ();

                int groupId = 1;
                if (i < 5)
                    groupId = 1;
                else if (i > 5 && i <= 10)
                    groupId = 2;
                else if (i > 10 && i <= 15)
                    groupId = 3;
                else if (i > 20)
                    groupId = 4;

                if (groupIdAux != groupId) {
                    userGameByGroup.Group = $"Grupo {groupId}";
                }
                
                userGameByGroup.Games = new List<GameViewModel> ();
                userGameByGroup.Games.Add

                userGame.UserId = 1;
                userGame.OficialGameId = i;

                userGame.User = new UserModel ();
                userGame.User.Id = 1;
                userGame.User.Name = "Fernando José";

                userGame.OficialGame = new OficialGameModel ();
                userGame.OficialGame.Id = i;

                userGame.OficialGame.GroupId = groupId;

                userGame.OficialGame.Group = new GroupModel ();
                userGame.OficialGame.Group.Id = groupId;
                userGame.OficialGame.Group.Name = groupId.ToString ();

                userGame.OficialGame.TeamA = new TeamModel ();
                userGame.OficialGame.TeamA.Id = i;
                userGame.OficialGame.TeamA.Name = $"Time {i}";

                userGame.OficialGame.TeamB = new TeamModel ();
                userGame.OficialGame.TeamB.Id = i;
                userGame.OficialGame.TeamB.Name = $"Time {i}";

                response.Add (userGame);
            }

            return response;

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userGameSql.SqlListByUserId ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@UserId", GetDbValue (userId));

                    conn.Open ();
                    using (DbDataReader dr = cmd.ExecuteReader ()) {
                        while (dr.Read ()) {
                            var userGame = new UserGameModel ();
                            userGame.Id = Convert.ToInt32 (dr["Id"].ToString ());
                            userGame.UserId = Convert.ToInt32 (dr["UserId"].ToString ());
                            userGame.OficialGameId = Convert.ToInt32 (dr["OficialGameId"].ToString ());
                            userGame.ScoreTeamA = Convert.ToInt32 (dr["ScoreTeamA"].ToString ());
                            userGame.ScoreTeamB = Convert.ToInt32 (dr["ScoreTeamB"].ToString ());
                            userGame.Points = Convert.ToInt32 (dr["Points"].ToString ());

                            userGame.User = new UserModel ();
                            userGame.User.Id = Convert.ToInt32 (dr["UserId"].ToString ());
                            userGame.User.Name = dr["UserName"].ToString ();

                            userGame.OficialGame = new OficialGameModel ();
                            userGame.OficialGame.Id = Convert.ToInt32 (dr["GameId"].ToString ());
                            userGame.OficialGame.GroupId = Convert.ToInt32 (dr["GroupId"].ToString ());
                            userGame.OficialGame.TeamAId = Convert.ToInt32 (dr["TeamAId"].ToString ());
                            userGame.OficialGame.TeamBId = Convert.ToInt32 (dr["TeamBId"].ToString ());

                            userGame.OficialGame.Group = new GroupModel ();
                            userGame.OficialGame.Group.Id = Convert.ToInt32 (dr["GroupId"].ToString ());
                            userGame.OficialGame.Group.Name = dr["GroupName"].ToString ();

                            userGame.OficialGame.TeamA = new TeamModel ();
                            userGame.OficialGame.TeamA.Id = Convert.ToInt32 (dr["TeamAId"].ToString ());
                            userGame.OficialGame.TeamA.Name = dr["TeamAName"].ToString ();

                            userGame.OficialGame.TeamB = new TeamModel ();
                            userGame.OficialGame.TeamB.Id = Convert.ToInt32 (dr["TeamBId"].ToString ());
                            userGame.OficialGame.TeamB.Name = dr["TeamBName"].ToString ();

                            response.Add (userGame);
                        }
                    }
                }
            }

            return response;
        }
    }
}