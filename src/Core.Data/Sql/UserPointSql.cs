namespace Core.Data.Sql
{
    public class UserPointSql
    {
        public string SqlCreate(){
            return "INSERT INTO BolaoUserPoint (UserId, OficialGameId, PointTypeId) " +
                   "VALUES (@UserId, @OficialGameId, @PointTypeId)";
        }

        public string SqlDeleteByOficialGameId(){
            return " DELETE FROM BolaoUserPoint " +
                   " WHERE OficialGameId = @OficialGameId";
        }

        public string SqlList(){
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