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
                    cmd.Parameters.AddWithValue ("@GameId", GetDbValue (request.GameId));
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
                    cmd.Parameters.AddWithValue ("@GameId", GetDbValue (request.GameId));
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
                            response.GameId = Convert.ToInt32 (dr["GameId"].ToString ());
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
                            userGame.GameId = Convert.ToInt32 (dr["GameId"].ToString ());
                            userGame.ScoreTeamA = Convert.ToInt32 (dr["ScoreTeamA"].ToString ());
                            userGame.ScoreTeamB = Convert.ToInt32 (dr["ScoreTeamB"].ToString ());
                            userGame.Points = Convert.ToInt32 (dr["Points"].ToString ());

                            userGame.User = new UserModel ();
                            userGame.User.Id = Convert.ToInt32 (dr["UserId"].ToString ());
                            userGame.User.Name = dr["UserName"].ToString ();

                            userGame.Game = new GameModel ();
                            userGame.Game.Id = Convert.ToInt32 (dr["GameId"].ToString ());
                            userGame.Game.GroupId = Convert.ToInt32 (dr["GroupId"].ToString ());
                            userGame.Game.TeamAId = Convert.ToInt32 (dr["TeamAId"].ToString ());
                            userGame.Game.TeamBId = Convert.ToInt32 (dr["TeamBId"].ToString ());

                            userGame.Game.Group = new GroupModel ();
                            userGame.Game.Group.Id = Convert.ToInt32 (dr["GroupId"].ToString ());
                            userGame.Game.Group.Name = dr["GroupName"].ToString ();

                            userGame.Game.TeamA = new TeamModel ();
                            userGame.Game.TeamA.Id = Convert.ToInt32 (dr["TeamAId"].ToString ());
                            userGame.Game.TeamA.Name = dr["TeamAName"].ToString ();

                            userGame.Game.TeamB = new TeamModel ();
                            userGame.Game.TeamB.Id = Convert.ToInt32 (dr["TeamBId"].ToString ());
                            userGame.Game.TeamB.Name = dr["TeamBName"].ToString ();

                            response.Add (userGame);
                        }
                    }
                }
            }

            return response;
        }

        public List<UserGameModel> ListByUser (UserGameModel request) {
            var response = new List<UserGameModel> ();

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userGameSql.SqlListByUser ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@UserId", GetDbValue (request.UserId));

                    conn.Open ();
                    using (DbDataReader dr = cmd.ExecuteReader ()) {
                        while (dr.Read ()) {
                            var userGame = new UserGameModel ();
                            userGame.Id = Convert.ToInt32 (dr["Id"].ToString ());
                            userGame.UserId = Convert.ToInt32 (dr["UserId"].ToString ());
                            userGame.GameId = Convert.ToInt32 (dr["GameId"].ToString ());
                            userGame.ScoreTeamA = Convert.ToInt32 (dr["ScoreTeamA"].ToString ());
                            userGame.ScoreTeamB = Convert.ToInt32 (dr["ScoreTeamB"].ToString ());
                            userGame.Points = Convert.ToInt32 (dr["Points"].ToString ());

                            userGame.User = new UserModel ();
                            userGame.User.Id = Convert.ToInt32 (dr["UserId"].ToString ());
                            userGame.User.Name = dr["UserName"].ToString ();

                            userGame.Game = new GameModel ();
                            userGame.Game.Id = Convert.ToInt32 (dr["GameId"].ToString ());
                            userGame.Game.GroupId = Convert.ToInt32 (dr["GroupId"].ToString ());
                            userGame.Game.TeamAId = Convert.ToInt32 (dr["TeamAId"].ToString ());
                            userGame.Game.TeamBId = Convert.ToInt32 (dr["TeamBId"].ToString ());

                            userGame.Game.Group = new GroupModel ();
                            userGame.Game.Group.Id = Convert.ToInt32 (dr["GroupId"].ToString ());
                            userGame.Game.Group.Name = dr["GroupName"].ToString ();

                            userGame.Game.TeamA = new TeamModel ();
                            userGame.Game.TeamA.Id = Convert.ToInt32 (dr["TeamAId"].ToString ());
                            userGame.Game.TeamA.Name = dr["TeamAName"].ToString ();

                            userGame.Game.TeamB = new TeamModel ();
                            userGame.Game.TeamB.Id = Convert.ToInt32 (dr["TeamBId"].ToString ());
                            userGame.Game.TeamB.Name = dr["TeamBName"].ToString ();

                            response.Add (userGame);
                        }
                    }
                }
            }

            return response;
        }
    }
}