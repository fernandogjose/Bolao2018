namespace Core.Data.Sql
{
    public class UserGameSql
    {
        public string SqlCreate(){
            return "INSERT INTO BolaoUserGame (UserId, GameId, ScoreTeamA, ScoreTeamB, Points) " +
                   "VALUES (@UserId, @GameId, @ScoreTeamA, @ScoreTeamB, @Points) SELECT @@IDENTITY";
        }

        public string SqlUpdate(){
            return " UPDATE BolaoUserGame SET " +
                   "   UserId = @UserId " +
                   " , GameId = @GameId " +
                   " , ScoreTeamA = @ScoreTeamA " +
                   " , ScoreTeamB = @ScoreTeamB " +
                   " , Points = @Points " +
                   " WHERE Id = @Id";
        }

        public string SqlGet(){
            return " SELECT BolaoUser.Id as UserId" +
                   "      , BolaoUser.Name as UserName " +
                   "      , BolaoUser.Email as UserEmail " +
                   "      , BolaoUser.Password as UserPassword " +
                   "      , BolaoGame.Id as GameId " +
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
                   " INNER JOIN BolaoGame ON BolaoUserGame.GameId = BolaoGame.Id" +
                   " INNER JOIN BolaoGroup ON BolaoGame.GroupId = BolaoGroup.Id" + 
                   " INNER JOIN BolaoTeam as BolaoTeamA ON BolaoGame.BolaoTeamAId = BolaoTeamA.Id" +
                   " INNER JOIN BolaoTeam as BolaoTeamB ON BolaoGame.BolaoTeamBId = BolaoTeamB.Id" + 
                   " WHERE Id = @Id";
        }

        public string SqlListByUser(){
            return " SELECT BolaoUser.Id as UserId" +
                   "      , BolaoUser.Name as UserName " +
                   "      , BolaoUser.Email as UserEmail " +
                   "      , BolaoUser.Password as UserPassword " +
                   "      , BolaoGame.Id as GameId " +
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
                   " INNER JOIN BolaoGame ON BolaoUserGame.GameId = BolaoGame.Id" +
                   " INNER JOIN BolaoGroup ON BolaoGame.GroupId = BolaoGroup.Id" + 
                   " INNER JOIN BolaoTeam as BolaoTeamA ON BolaoGame.BolaoTeamAId = BolaoTeamA.Id" +
                   " INNER JOIN BolaoTeam as BolaoTeamB ON BolaoGame.BolaoTeamBId = BolaoTeamB.Id" + 
                   " WHERE UserId = @UserId";
        }

        public string SqlList(){
            return " SELECT BolaoUser.Id as UserId" +
                   "      , BolaoUser.Name as UserName " +
                   "      , BolaoUser.Email as UserEmail " +
                   "      , BolaoUser.Password as UserPassword " +
                   "      , BolaoGame.Id as GameId " +
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
                   " INNER JOIN BolaoGame ON BolaoUserGame.GameId = BolaoGame.Id" +
                   " INNER JOIN BolaoGroup ON BolaoGame.GroupId = BolaoGroup.Id" + 
                   " INNER JOIN BolaoTeam as BolaoTeamA ON BolaoGame.BolaoTeamAId = BolaoTeamA.Id" +
                   " INNER JOIN BolaoTeam as BolaoTeamB ON BolaoGame.BolaoTeamBId = BolaoTeamB.Id";
        }
    }
}