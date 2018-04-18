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

        public User Create (User request) {

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

        public User Update (User request) {

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

        public User Login (string email, string password) {
            var response = new User ();

            // response.Id = 1;
            // response.Email = "fernandogjose@gmail.com";
            // response.Name = "Fernando José";
            // response.Token = "dskfjhasdjkfhf8s9ad7f897df89sa7f897fds";

            // return response;

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userSql.SqlLogin ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@Email", GetDbValue (email));
                    cmd.Parameters.AddWithValue ("@Password", GetDbValue (password));

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

        public User GetById (int id) {
            var response = new User ();

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userSql.SqlGetById ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@Id", GetDbValue (id));

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

        public User GetByEmail (string email) {
            var response = new User ();

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _userSql.SqlGetByEmail ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@Email", GetDbValue (email));

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