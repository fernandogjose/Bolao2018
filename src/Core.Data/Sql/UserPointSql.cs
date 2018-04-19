namespace Core.Data.Sql {
    public class UserPointSql {
        public string SqlCreate () {
            return "INSERT INTO BolaoUserPoint (UserId, OficialGameId, PointTypeId) " +
                "VALUES (@UserId, @OficialGameId, @PointTypeId)";
        }

        public string SqlDeleteByOficialGameId () {
            return " DELETE FROM BolaoUserPoint " +
                " WHERE OficialGameId = @OficialGameId";
        }

        public string SqlList () {
            return " SELECT UserPoint.UserId " +
                "    	   ,User.Name as UserName  " +
                "    	   ,ISNULL((SELECT COUNT(PointTypeId) FROM BolaoUserPoint WHERE UserId = UserPoint.UserId and PointTypeId = 1), 0) as VCPC  " +
                "    	   ,ISNULL((SELECT COUNT(PointTypeId) FROM BolaoUserPoint WHERE UserId = UserPoint.UserId and PointTypeId = 2), 0) as VCUPC " +
                "    	   ,ISNULL((SELECT COUNT(PointTypeId) FROM BolaoUserPoint WHERE UserId = UserPoint.UserId and PointTypeId = 3), 0) as VCPE  " +
                "    	   ,ISNULL((SELECT COUNT(PointTypeId) FROM BolaoUserPoint WHERE UserId = UserPoint.UserId and PointTypeId = 4), 0) as ECPC  " +
                "    	   ,ISNULL((SELECT COUNT(PointTypeId) FROM BolaoUserPoint WHERE UserId = UserPoint.UserId and PointTypeId = 5), 0) as ECPE  " +
                "    	   ,ISNULL((SELECT COUNT(PointTypeId) FROM BolaoUserPoint WHERE UserId = UserPoint.UserId and PointTypeId = 6), 0) as RE    " +
                "          ,SUM(PointType.Points) AS Total" +
                "    FROM BolaoUserPoint as UserPoint " +
                "    INNER JOIN BolaoUser ON (User ON UserPoint.UserId = User.Id) " +
                "    INNER JOIN PointType ON (UserPoint.PointTypeId = PointType.Id) " +
                "    GROUP BY UserPoint.UserId " +
                "            ,User.Name " +
                "    ORDER BY SUM(PointType.Points) DESC" + 
                "            ,UserPoint.UserId ";
        }
    }
}