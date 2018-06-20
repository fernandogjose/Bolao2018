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
            return " SELECT " +
                " 	     UserPoint.UserId  " +
                "       ,BolaoUser.Name as UserName   " +
                " 	    ,SUM(PointType.Points) AS Total" +
                " 	    ,ISNULL((SELECT COUNT(BolaoUserPoint.PointTypeId) FROM BolaoUserPoint WHERE BolaoUserPoint.UserId = UserPoint.UserId and BolaoUserPoint.PointTypeId = 1), 0) as VCPC   " +
                "       ,ISNULL((SELECT COUNT(BolaoUserPoint.PointTypeId) FROM BolaoUserPoint WHERE BolaoUserPoint.UserId = UserPoint.UserId and BolaoUserPoint.PointTypeId = 2), 0) as VCUPC  " +
                "       ,ISNULL((SELECT COUNT(BolaoUserPoint.PointTypeId) FROM BolaoUserPoint WHERE BolaoUserPoint.UserId = UserPoint.UserId and BolaoUserPoint.PointTypeId = 3), 0) as VCPE   " +
                "       ,ISNULL((SELECT COUNT(BolaoUserPoint.PointTypeId) FROM BolaoUserPoint WHERE BolaoUserPoint.UserId = UserPoint.UserId and BolaoUserPoint.PointTypeId = 4), 0) as ECPC   " +
                "       ,ISNULL((SELECT COUNT(BolaoUserPoint.PointTypeId) FROM BolaoUserPoint WHERE BolaoUserPoint.UserId = UserPoint.UserId and BolaoUserPoint.PointTypeId = 5), 0) as ECPE   " +
                "       ,ISNULL((SELECT COUNT(BolaoUserPoint.PointTypeId) FROM BolaoUserPoint WHERE BolaoUserPoint.UserId = UserPoint.UserId and BolaoUserPoint.PointTypeId = 6), 0) as RE     " +
                " FROM BolaoUserPoint as UserPoint  " +
                " INNER JOIN BolaoUser ON (UserPoint.UserId = BolaoUser.Id)  " +
                " INNER JOIN BolaoPointType as PointType ON (UserPoint.PointTypeId = PointType.Id)  " +
                " GROUP BY UserPoint.UserId  " +
                "         ,BolaoUser.Name " +
                " ORDER BY SUM(PointType.Points) DESC  " +
                "      ,ISNULL((SELECT COUNT(BolaoUserPoint.PointTypeId) FROM BolaoUserPoint WHERE BolaoUserPoint.UserId = UserPoint.UserId and BolaoUserPoint.PointTypeId = 4), 0) DESC " +
                " 	   ,ISNULL((SELECT COUNT(BolaoUserPoint.PointTypeId) FROM BolaoUserPoint WHERE BolaoUserPoint.UserId = UserPoint.UserId and BolaoUserPoint.PointTypeId = 1), 0) DESC " +
                "      ,ISNULL((SELECT COUNT(BolaoUserPoint.PointTypeId) FROM BolaoUserPoint WHERE BolaoUserPoint.UserId = UserPoint.UserId and BolaoUserPoint.PointTypeId = 2), 0) DESC " +
                "      ,ISNULL((SELECT COUNT(BolaoUserPoint.PointTypeId) FROM BolaoUserPoint WHERE BolaoUserPoint.UserId = UserPoint.UserId and BolaoUserPoint.PointTypeId = 5), 0) DESC " +
                "      ,ISNULL((SELECT COUNT(BolaoUserPoint.PointTypeId) FROM BolaoUserPoint WHERE BolaoUserPoint.UserId = UserPoint.UserId and BolaoUserPoint.PointTypeId = 3), 0) DESC ";
        }
    }
}