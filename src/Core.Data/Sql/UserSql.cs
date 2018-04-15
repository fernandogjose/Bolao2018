namespace Core.Data.Sql
{
    public class UserSql
    {
        public string SqlCreate(){
            return "INSERT INTO BolaoUser (Name, Email, Password) " +
                   "VALUES (@Name, @Email, @Password) SELECT @@IDENTITY";
        }

        public string SqlUpdate(){
            return " UPDATE BolaoUser SET " +
                   "   Name = @Name " +
                   " , Email = @Email " +
                   " , Password = @Password " +
                   " WHERE Id = @Id";
        }

        public string SqlGetById(){
            return " SELECT BolaoUser.Id " +
                   "      , BolaoUser.Name " +
                   "      , BolaoUser.Email " +
                   "      , BolaoUser.Password " +
                   " FROM BolaoUser " +
                   " WHERE Id = @Id";
        }

        public string SqlGetByEmail(){
            return " SELECT BolaoUser.Id " +
                   "      , BolaoUser.Name " +
                   "      , BolaoUser.Email " +
                   "      , BolaoUser.Password " +
                   " FROM BolaoUser " +
                   " WHERE Email = @Email";
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