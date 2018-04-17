namespace Core.Data.Sql {
    public class UserGameSql {
        public string SqlCreate () {
            return "INSERT INTO BolaoUserGame (UserId, GameId, ScoreTeamA, ScoreTeamB, Points) " +
                "VALUES (@UserId, @GameId, @ScoreTeamA, @ScoreTeamB, @Points) SELECT @@IDENTITY";
        }

        public string SqlUpdate () {
            return " UPDATE BolaoUserGame SET " +
                "   UserId = @UserId " +
                " , GameId = @GameId " +
                " , ScoreTeamA = @ScoreTeamA " +
                " , ScoreTeamB = @ScoreTeamB " +
                " , Points = @Points " +
                " WHERE Id = @Id";
        }

        public string SqlGet () {
            return " SELECT BolaoUser.Id as UserId" +
                "      , BolaoUser.Name as UserName " +
                "      , BolaoUser.Email as UserEmail " +
                "      , BolaoUser.Password as UserPassword " +
                "      , BolaoOficialGame.Id as GameId " +
                "      , BolaoGroup.Id as GroupId " +
                "      , BolaoGroup.Name as GroupName " +
                "      , BolaoTeamA.Id as TeamAId " +
                "      , BolaoTeamA.Name as TeamAName " +
                "      , BolaoTeamB.Id as TeamBId " +
                "      , BolaoTeamB.Name as TeamBName " +
                "      , BolaoUserGame.ScoreTeamA as BolaoUserGameScoreTeamA " +
                "      , BolaoUserGame.ScoreTeamB as BolaoUserGameScoreTeamB " +
                "      , BolaoUserGame.Points as BolaoUserGamePoints " +
                " FROM BolaoUserGame " +
                " INNER JOIN BolaoUser ON BolaoUserGame.UserId = BolaoUser.Id" +
                " INNER JOIN BolaoOficialGame ON BolaoUserGame.GameId = BolaoOficialGame.Id" +
                " INNER JOIN BolaoGroup ON BolaoOficialGame.GroupId = BolaoGroup.Id" +
                " INNER JOIN BolaoTeam as BolaoTeamA ON BolaoOficialGame.BolaoTeamAId = BolaoTeamA.Id" +
                " INNER JOIN BolaoTeam as BolaoTeamB ON BolaoOficialGame.BolaoTeamBId = BolaoTeamB.Id" +
                " WHERE Id = @Id";
        }

        public string SqlListByUserId () {
            return " SELECT BolaoGroup.[Name] as GroupName" +
                   "      , BolaoOficialGame.Id as OficialGameId" +
                   "      , BolaoOficialGame.[Date] as GameDate" +
                   "      , BolaoTeamA.[Name] as TeamA" +
                   "      , BolaoTeamB.[Name] as TeamB" +
                   "      , ISNULL((SELECT ScoreTeamA FROM BolaoUserGame WHERE OficialGameId = BolaoOficialGame.Id AND UserId = @UserId), 0) as ScoreTeamA" +
                   "      , ISNULL((SELECT ScoreTeamB FROM BolaoUserGame WHERE OficialGameId = BolaoOficialGame.Id AND UserId = @UserId), 0) as ScoreTeamB" +
                   " FROM BolaoOficialGame" +
                   " INNER JOIN BolaoGroup ON BolaoOficialGame.GroupId = BolaoGroup.Id" +
                   " INNER JOIN BolaoTeam as BolaoTeamA ON BolaoOficialGame.TeamAId = BolaoTeamA.Id" +
                   " INNER JOIN BolaoTeam as BolaoTeamB ON BolaoOficialGame.TeamBId = BolaoTeamB.Id" +
                   " ORDER BY BolaoGroup.[Name]" +
                   "         ,BolaoOficialGame.[Date]";
        }

        public string SqlList () {
            return " SELECT BolaoUser.Id as UserId" +
                "      , BolaoUser.Name as UserName " +
                "      , BolaoUser.Email as UserEmail " +
                "      , BolaoUser.Password as UserPassword " +
                "      , BolaoOficialGame.Id as GameId " +
                "      , BolaoGroup.Id as GroupId " +
                "      , BolaoGroup.Name as GroupName " +
                "      , BolaoTeamA.Id as TeamAId " +
                "      , BolaoTeamA.Name as TeamAName " +
                "      , BolaoTeamB.Id as TeamBId " +
                "      , BolaoTeamB.Name as TeamBName " +
                "      , BolaoUserGame.ScoreTeamA as BolaoUserGameScoreTeamA " +
                "      , BolaoUserGame.ScoreTeamB as BolaoUserGameScoreTeamB " +
                "      , BolaoUserGame.Points as BolaoUserGamePoints " +
                " FROM BolaoUserGame " +
                " INNER JOIN BolaoUser ON BolaoUserGame.UserId = BolaoUser.Id" +
                " INNER JOIN BolaoOficialGame ON BolaoUserGame.GameId = BolaoOficialGame.Id" +
                " INNER JOIN BolaoGroup ON BolaoOficialGame.GroupId = BolaoGroup.Id" +
                " INNER JOIN BolaoTeam as BolaoTeamA ON BolaoOficialGame.BolaoTeamAId = BolaoTeamA.Id" +
                " INNER JOIN BolaoTeam as BolaoTeamB ON BolaoOficialGame.BolaoTeamBId = BolaoTeamB.Id";
        }
    }
}