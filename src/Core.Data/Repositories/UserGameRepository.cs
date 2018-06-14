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

        private bool CanChangeMyHunch (string groupName) {
            if (groupName == "Grupo A" ||
                groupName == "Grupo B" ||
                groupName == "Grupo C" ||
                groupName == "Grupo D" ||
                groupName == "Grupo E" ||
                groupName == "Grupo F" ||
                groupName == "Grupo G" ||
                groupName == "Grupo H") {
                return false;
            }

            // if (groupName == "Oitavas de final" && new DateTime (DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) > new DateTime (2018, 6, 29)) {
            //     return false;
            // }

            // if (groupName == "Quartas de final" && new DateTime (DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) > new DateTime (2018, 7, 5)) {
            //     return false;
            // }

            // if (groupName == "Semifinal" && new DateTime (DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) > new DateTime (2018, 7, 9)) {
            //     return false;
            // }

            // if (groupName == "3º lugar" && new DateTime (DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) > new DateTime (2018, 7, 13)) {
            //     return false;
            // }

            // if (groupName == "Final" && new DateTime (DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) > new DateTime (2018, 7, 14)) {
            //     return false;
            // }

            return true;
        }

        public void Save (List<GameByGroup> gamesByGroupRequest) {
            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                conn.Open ();

                foreach (var gameByGroupRequest in gamesByGroupRequest) {
                    foreach (var gameOfGroupRequest in gameByGroupRequest.Games) {

                        if (!CanChangeMyHunch (gameByGroupRequest.GroupName))
                            continue;

                        using (var cmd = new SqlCommand ()) {
                            cmd.Connection = conn;
                            cmd.CommandText = _userGameSql.SqlSave ();
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue ("@UserId", GetDbValue (gameOfGroupRequest.UserId));
                            cmd.Parameters.AddWithValue ("@OficialGameId", GetDbValue (gameOfGroupRequest.OficialGameId));
                            cmd.Parameters.AddWithValue ("@ScoreTeamA", GetDbValue (gameOfGroupRequest.ScoreTeamA));
                            cmd.Parameters.AddWithValue ("@ScoreTeamB", GetDbValue (gameOfGroupRequest.ScoreTeamB));
                            cmd.ExecuteNonQuery ();
                        }
                    }
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
                            gameOfGroup.UserName = dr["UserName"].ToString ();

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
                    game.CanChangeMyHunch = CanChangeMyHunch (userGameByGroup.GroupName);
                    game.UserId = userId;
                    userGameByGroup.Games.Add (game);
                }

                gamesByGroupResponse.Add (userGameByGroup);
            }

            return gamesByGroupResponse;
        }

        public List<UserGameScore> ListByOficialGameId (int oficialGameId) {
            List<UserGameScore> userGameScoresResponse = new List<UserGameScore> ();

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userGameSql.SqlListByOficialGameId ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@OficialGameId", GetDbValue (oficialGameId));

                    conn.Open ();
                    using (DbDataReader dr = cmd.ExecuteReader ()) {
                        while (dr.Read ()) {

                            UserGameScore userGameScore = new UserGameScore ();
                            userGameScore.UserId = Convert.ToInt32 (dr["UserId"].ToString ());
                            userGameScore.OficialGameId = oficialGameId;
                            userGameScore.ScoreTeamA = Convert.ToInt32 (dr["ScoreTeamA"].ToString ());
                            userGameScore.ScoreTeamB = Convert.ToInt32 (dr["ScoreTeamB"].ToString ());

                            userGameScoresResponse.Add (userGameScore);
                        }
                    }
                }
            }

            return userGameScoresResponse;
        }
    }
}