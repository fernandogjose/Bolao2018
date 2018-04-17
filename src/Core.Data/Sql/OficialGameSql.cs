namespace Core.Data.Sql {
    public class OficialGameSql {

        public string SqlGet () {
            return "SELECT * FROM BolaoOficialGame WHERE Id = @Id";
        }

        public string SqlCreate () {
            return " INSERT INTO BolaoUserGame (UserId, OficialGameId, ScoreTeamA, ScoreTeamB) " +
                "                       VALUES (@UserId, @OficialGameId, @ScoreTeamA, @ScoreTeamB) " + 
                "    SELECT @@IDENTITY";
        }

        public string SqlUpdatePoints () {
            return " UPDATE BolaoUserGame SET " +
                "         , Points = @Points " +
                "    WHERE UserId = @UserId" +
                "      AND OficialGameId = @OficialGameId";
        }

        public string SqlListByUserId () {
            return " SELECT BolaoGroup.[Name] as GroupName" +
                "         , BolaoOficialGame.Id as OficialGameId" +
                "         , BolaoOficialGame.[Date] as GameDate" +
                "         , BolaoTeamA.[Name] as TeamA" +
                "         , BolaoTeamB.[Name] as TeamB" +
                "         , ISNULL((SELECT ScoreTeamA FROM BolaoUserGame WHERE OficialGameId = BolaoOficialGame.Id AND UserId = @UserId), 0) as ScoreTeamA" +
                "         , ISNULL((SELECT ScoreTeamB FROM BolaoUserGame WHERE OficialGameId = BolaoOficialGame.Id AND UserId = @UserId), 0) as ScoreTeamB" +
                "    FROM BolaoOficialGame" +
                "    INNER JOIN BolaoGroup ON BolaoOficialGame.GroupId = BolaoGroup.Id" +
                "    INNER JOIN BolaoTeam as BolaoTeamA ON BolaoOficialGame.TeamAId = BolaoTeamA.Id" +
                "    INNER JOIN BolaoTeam as BolaoTeamB ON BolaoOficialGame.TeamBId = BolaoTeamB.Id" +
                "    ORDER BY BolaoGroup.[Name]" +
                "            ,BolaoOficialGame.[Date]";
        }
    }
}