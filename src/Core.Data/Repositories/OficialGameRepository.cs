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
    }
}