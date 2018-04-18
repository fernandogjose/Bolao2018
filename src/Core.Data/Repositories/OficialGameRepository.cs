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
    public class OficialGameRepository : BaseRepository, IOficialGameRepository {

        private readonly OficialGameSql _oficialGameSql;

        public OficialGameRepository (OficialGameSql oficialGameSql) {
            _oficialGameSql = oficialGameSql;
        }

        public OficialGame Get (int id) {
            OficialGame oficialGameResponse = new OficialGame ();

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _oficialGameSql.SqlGet ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@Id", GetDbValue (id));

                    conn.Open ();
                    using (DbDataReader dr = cmd.ExecuteReader ()) {
                        if (dr.Read ()) {
                            oficialGameResponse.Id = Convert.ToInt32 (dr["Id"].ToString ());
                            oficialGameResponse.Date = Convert.ToDateTime (dr["Date"].ToString ());
                        }
                    }
                }
            }

            return oficialGameResponse;
        }

        public List<GameByGroup> List () {
            List<GameOfGroup> gamesOfGroup = new List<GameOfGroup> ();

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _oficialGameSql.SqlList ();
                    cmd.CommandType = CommandType.Text;

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