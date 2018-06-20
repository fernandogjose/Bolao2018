namespace Core.Data.Sql {
    public class ReportSql {
        public string SqlListBet () {
            return  " SELECT BolaoOficialGame.Id as OficialGameId " +
                    " 	  ,BolaoOficialGame.Date as OficialGameDate " +
                    " 	  ,BolaoGroup.Name as GroupName " +
                    " 	  ,TeamA.Name as TeamAName " +
                    " 	  ,TeamB.Name as TeamBName " +
                    " 	  ,ISNULL((SELECT COUNT(BolaoUserGame.Id) FROM BolaoUserGame WHERE BolaoUserGame.ScoreTeamA > BolaoUserGame.ScoreTeamB AND BolaoUserGame.OficialGameId = BolaoOficialGame.Id GROUP BY BolaoUserGame.OficialGameId), 0) as WinTeamA " +
                    " 	  ,ISNULL((SELECT COUNT(BolaoUserGame.Id) FROM BolaoUserGame WHERE BolaoUserGame.ScoreTeamA < BolaoUserGame.ScoreTeamB AND BolaoUserGame.OficialGameId = BolaoOficialGame.Id GROUP BY BolaoUserGame.OficialGameId), 0) as WinTeamB " +
                    " 	  ,ISNULL((SELECT COUNT(BolaoUserGame.Id) FROM BolaoUserGame WHERE BolaoUserGame.ScoreTeamA = BolaoUserGame.ScoreTeamB AND BolaoUserGame.OficialGameId = BolaoOficialGame.Id GROUP BY BolaoUserGame.OficialGameId), 0) as Draw " +
                    " FROM BolaoOficialGame " +
                    " INNER JOIN BolaoGroup on BolaoGroup.Id = BolaoOficialGame.GroupId " +
                    " INNER JOIN BolaoTeam as TeamA on TeamA.Id = BolaoOficialGame.TeamAId " +
                    " INNER JOIN BolaoTeam as TeamB on TeamB.Id = BolaoOficialGame.TeamBId " +
                    " GROUP BY BolaoOficialGame.Id " +
                    " 		,BolaoOficialGame.Date " +
                    " 		,BolaoGroup.Id " +
                    " 		,BolaoGroup.Name " +
                    " 		,TeamA.Name " +
                    " 		,TeamB.Name " +
                    " ORDER BY BolaoGroup.Id " +
                    " 		,BolaoOficialGame.Date";
        }

        public string SqlListBetDetail () {
            return  " SELECT BolaoOficialGame.Id as OficialGameId " +
                    " 	  ,BolaoOficialGame.Date as OficialGameDate " +
                    " 	  ,BolaoGroup.Name as GroupName " +
                    " 	  ,TeamA.Name as TeamAName " +
                    " 	  ,TeamB.Name as TeamBName " +
                    " 	  ,BolaoUserGame.ScoreTeamA " +
                    " 	  ,BolaoUserGame.ScoreTeamB " +
                    " 	  ,BolaoUser.Name as UserName" +
                    " FROM BolaoOficialGame " +
                    " INNER JOIN BolaoGroup on BolaoGroup.Id = BolaoOficialGame.GroupId " +
                    " INNER JOIN BolaoTeam as TeamA on TeamA.Id = BolaoOficialGame.TeamAId " +
                    " INNER JOIN BolaoTeam as TeamB on TeamB.Id = BolaoOficialGame.TeamBId " +
                    " INNER JOIN BolaoUserGame on BolaoUserGame.OficialGameId = BolaoOficialGame.Id " +
                    " INNER JOIN BolaoUser on BolaoUser.Id = BolaoUserGame.UserId " +
                    " WHERE BolaoOficialGame.Id = @OficialGameId";
        }
    }
}