using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Core.Data.Sql;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Models;

namespace Core.Data.Repositories {
    public class ChatRepository : BaseRepository, IChatRepository {
        private readonly ChatSql _chatSql;

        public ChatRepository (ChatSql chatSql) {
            _chatSql = chatSql;
        }

        public void Create (ChatCreateRequest chatCreateRequest) {
            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                conn.Open ();

                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _chatSql.SqlCreate ();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue ("@UserId", GetDbValue (chatCreateRequest.UserId));
                    cmd.Parameters.AddWithValue ("@Message", GetDbValue (chatCreateRequest.Message));
                    cmd.ExecuteNonQuery ();
                }
            }
        }

        public List<Chat> List () {
            List<Chat> chatsResponse = new List<Chat> ();

            using (SqlConnection conn = new SqlConnection (ConnectionString ())) {
                using (var cmd = new SqlCommand ()) {
                    cmd.Connection = conn;
                    cmd.CommandText = _chatSql.SqlList ();
                    cmd.CommandType = CommandType.Text;

                    conn.Open ();
                    using (DbDataReader dr = cmd.ExecuteReader ()) {
                        while (dr.Read ()) {
                            Chat chat = new Chat ();
                            chat.Message = dr["Message"].ToString ();
                            chat.Date = Convert.ToDateTime (dr["Date"].ToString ());
                            chat.User = new User ();
                            chat.User.Name = dr["UserName"].ToString ();
                            chatsResponse.Add (chat);
                        }
                    }
                }
            }

            return chatsResponse;
        }
    }
}