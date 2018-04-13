namespace Core.Data.Sql
{
    public class UserSql
    {
        public string SqlCreate(){
            return "INSERT INTO BolaoUser (Name, Email, Password, Token) " +
                   "VALUES (@Name, @Email, @Password, @Token) SELECT @@IDENTITY";
        }

        public string SqlUpdate(){
            return " UPDATE BolaoUser SET " +
                   "   Name = @Name " +
                   " , Email = @Email " +
                   " , Password = @Password " +
                   " WHERE Token = @Token";
        }

        public string SqlUpdateToken(){
            return " UPDATE BolaoUser SET " +
                   "   Token = @Token " +
                   " WHERE Id = @Id";
        }

        public string SqlGet(){
            return " SELECT BolaoUser.Id " +
                   "      , BolaoUser.Name " +
                   "      , BolaoUser.Email " +
                   "      , BolaoUser.Password " +
                   " FROM BolaoUser " +
                   " WHERE Token = @Token";
        }

        public string SqlLogin(){
            return " SELECT BolaoUser.Id " +
                   "      , BolaoUser.Name " +
                   "      , BolaoUser.Email " +
                   "      , BolaoUser.Password " +
                   " FROM BolaoUser " +
                   " WHERE Email = @Email" +
                   "   AND Password = @Password";
        }
    }
}