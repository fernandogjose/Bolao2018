using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Core.Data.Sql;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;

namespace Core.Data.Repositories {
    public class UserRepository : BaseRepository, IUserRepository {
        private readonly UserSql _userSql;

        public UserRepository (UserSql userSql) {
            _userSql = userSql;
        }

        public UserModel Create (UserModel request) {

            var response = request;

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userSql.SqlCreate ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@Name", GetDbValue (request.Name));
                    cmd.Parameters.AddWithValue ("@Email", GetDbValue (request.Email));
                    cmd.Parameters.AddWithValue ("@Password", GetDbValue (request.Password));

                    conn.Open ();
                    response.Id = Convert.ToInt32 (cmd.ExecuteScalar ());
                }
            }

            return response;
        }

        public UserModel Update (UserModel request) {

            var response = request;

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userSql.SqlUpdate ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@Id", GetDbValue (request.Id));
                    cmd.Parameters.AddWithValue ("@Name", GetDbValue (request.Name));
                    cmd.Parameters.AddWithValue ("@Email", GetDbValue (request.Email));
                    cmd.Parameters.AddWithValue ("@Password", GetDbValue (request.Password));
                    conn.Open ();
                    cmd.ExecuteNonQuery ();
                }
            }

            return response;
        }

        public UserModel Login (UserModel request) {
            var response = new UserModel ();

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userSql.SqlLogin ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@Email", GetDbValue (request.Email));
                    cmd.Parameters.AddWithValue ("@Password", GetDbValue (request.Password));

                    conn.Open ();
                    using (DbDataReader dr = cmd.ExecuteReader ()) {
                        if (dr.Read ()) {
                            response.Id = Convert.ToInt32 (dr["Id"].ToString ());
                            response.Name = dr["Name"].ToString ();
                            response.Email = dr["Email"].ToString ();
                        }
                    }
                }
            }

            return response;
        }

        public UserModel Get (UserModel request) {
            var response = new UserModel ();

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userSql.SqlGet ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@Id", GetDbValue (request.Id));

                    conn.Open ();
                    using (DbDataReader dr = cmd.ExecuteReader ()) {
                        if (dr.Read ()) {
                            response.Id = Convert.ToInt32 (dr["Id"].ToString ());
                            response.Name = dr["Name"].ToString ();
                            response.Email = dr["Email"].ToString ();
                        }
                    }
                }
            }

            return response;
        }
    }
}