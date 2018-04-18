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
    public class UserGameRepository : BaseRepository, IUserGameRepository {
        private readonly UserGameSql _userGameSql;

        public UserGameRepository (UserGameSql userGameSql) {
            _userGameSql = userGameSql;
        }

        public void DeleteByUserIdAndOficialGameId (int userId, int oficialGameId) {
            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userGameSql.SqlDelete ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@UserId", GetDbValue (userId));
                    cmd.Parameters.AddWithValue ("@UserId", GetDbValue (oficialGameId));

                    conn.Open ();
                    cmd.ExecuteNonQuery ();
                }
            }
        }

        public UserGame GetByUserIdAndOficialGameId (int userId, int oficialGameId) {
            UserGame userGame = new UserGame ();

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userGameSql.SqlGetByUserIdAndOficialGameId ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@UserId", GetDbValue (userId));
                    cmd.Parameters.AddWithValue ("@OficialGameId", GetDbValue (oficialGameId));

                    conn.Open ();
                    using (DbDataReader dr = cmd.ExecuteReader ()) {
                        if (dr.Read ()) {
                            userGame.OficialGameId = Convert.ToInt32 (dr["OficialGameId"].ToString ());
                            userGame.ScoreTeamA = Convert.ToInt32 (dr["ScoreTeamA"].ToString ());
                            userGame.ScoreTeamB = Convert.ToInt32 (dr["ScoreTeamB"].ToString ());
                            userGame.OficialGame = new OficialGame ();
                            userGame.OficialGame.Date = Convert.ToDateTime (dr["GameDate"].ToString ());
                        }
                    }
                }
            }

            return userGame;
        }

        public void Create (UserGameSaveRequest request) {
            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userGameSql.SqlCreate ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@UserId", GetDbValue (request.UserId));
                    cmd.Parameters.AddWithValue ("@OficialGameId", GetDbValue (request.OficialGameId));
                    cmd.Parameters.AddWithValue ("@ScoreTeamA", GetDbValue (request.ScoreTeamA));
                    cmd.Parameters.AddWithValue ("@ScoreTeamB", GetDbValue (request.ScoreTeamB));

                    conn.Open ();
                    cmd.ExecuteNonQuery ();
                }
            }
        }

        public void Update (UserGameSaveRequest request) {
            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userGameSql.SqlUpdate ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@UserId", GetDbValue (request.UserId));
                    cmd.Parameters.AddWithValue ("@OficialGameId", GetDbValue (request.OficialGameId));
                    cmd.Parameters.AddWithValue ("@ScoreTeamA", GetDbValue (request.ScoreTeamA));
                    cmd.Parameters.AddWithValue ("@ScoreTeamB", GetDbValue (request.ScoreTeamB));

                    conn.Open ();
                    cmd.ExecuteNonQuery ();
                }
            }
        }

        public UserGame UpdatePoints (UserGame request) {

            var response = request;

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userGameSql.SqlUpdatePoints ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@UserId", GetDbValue (request.UserId));
                    cmd.Parameters.AddWithValue ("@OficialGameId", GetDbValue (request.OficialGameId));
                    cmd.Parameters.AddWithValue ("@Points", GetDbValue (request.Points));

                    conn.Open ();
                    cmd.ExecuteNonQuery ();
                }
            }

            return response;
        }

        public List<GameByGroup> ListByUserId (int userId) {
            List<GameOfGroup> gamesOfGroup = new List<GameOfGroup> ();

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userGameSql.SqlListByUserId ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@UserId", GetDbValue (userId));

                    conn.Open ();
                    using (DbDataReader dr = cmd.ExecuteReader ()) {
                        while (dr.Read ()) {

                            GameOfGroup gameOfGroup = new GameOfGroup ();
                            gameOfGroup.OficialGameId = Convert.ToInt32 (dr["OficialGameId"].ToString ());
                            gameOfGroup.GameDate = Convert.ToDateTime (dr["GameDate"].ToString ());
                            gameOfGroup.TeamA = dr["TeamA"].ToString ();
                            gameOfGroup.TeamB = dr["TeamB"].ToString ();
                            gameOfGroup.GroupName = dr["GroupName"].ToString ();
                            gameOfGroup.ScoreTeamA = Convert.ToInt32 (dr["ScoreTeamA"].ToString ());
                            gameOfGroup.ScoreTeamB = Convert.ToInt32 (dr["ScoreTeamB"].ToString ());

                            gamesOfGroup.Add (gameOfGroup);
                        }
                    }
                }
            }

            //--- monta o objeto de retorno
            var gamesByGroupResponse = new List<GameByGroup> ();

            foreach (var group in gamesOfGroup.GroupBy (g => g.GroupName)) {
                GameByGroup userGameByGroup = new GameByGroup ();
                userGameByGroup.GroupName = group.Key;
                userGameByGroup.Games = new List<GameOfGroup> ();

                foreach (var game in gamesOfGroup.Where (m => m.GroupName == userGameByGroup.GroupName)) {
                    game.CanSave = DateTime.Now < game.GameDate.AddHours (-4);
                    userGameByGroup.Games.Add (game);
                }

                gamesByGroupResponse.Add (userGameByGroup);
            }

            return gamesByGroupResponse;
        }
    }
}