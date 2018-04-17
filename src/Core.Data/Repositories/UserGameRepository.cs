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

        public List<UserGameByGroupViewModel> ListByUserId (int userId) {
            List<GameViewModel> games = new List<GameViewModel> ();

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userGameSql.SqlListByUserId ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@UserId", GetDbValue (userId));

                    conn.Open ();
                    using (DbDataReader dr = cmd.ExecuteReader ()) {
                        while (dr.Read ()) {

                            GameViewModel game = new GameViewModel ();
                            game.OficialGameId = Convert.ToInt32 (dr["OficialGameId"].ToString ());
                            game.GameDate = Convert.ToDateTime (dr["GameDate"].ToString ()).ToString ("dd/MM/yyyy HH:mm");
                            game.TeamA = dr["TeamA"].ToString ();
                            game.TeamB = dr["TeamB"].ToString ();
                            game.GroupName = dr["GroupName"].ToString ();
                            game.ScoreTeamA = Convert.ToInt32 (dr["ScoreTeamA"].ToString ());
                            game.ScoreTeamB = Convert.ToInt32 (dr["ScoreTeamB"].ToString ());

                            games.Add (game);
                        }
                    }
                }
            }

            //--- monta o objeto de retorno
            var response = new List<UserGameByGroupViewModel> ();

            foreach (var group in games.GroupBy (g => g.GroupName)) {
                UserGameByGroupViewModel userGameByGroup = new UserGameByGroupViewModel ();
                userGameByGroup.GroupName = group.Key;
                userGameByGroup.Games = new List<GameViewModel> ();

                foreach (var game in games.Where (m => m.GroupName == userGameByGroup.GroupName)) {
                    userGameByGroup.Games.Add (new GameViewModel {
                        OficialGameId = game.OficialGameId,
                        GroupName = group.Key,
                        GameDate = game.GameDate,
                        TeamA = game.TeamA,
                        TeamB = game.TeamB,
                        ScoreTeamA = game.ScoreTeamA,
                        ScoreTeamB = game.ScoreTeamB,
                    });
                }

                response.Add(userGameByGroup);
            }

            return response;
        }
    }
}