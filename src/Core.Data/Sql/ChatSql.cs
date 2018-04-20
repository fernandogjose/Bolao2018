namespace Core.Data.Sql {
    public class ChatSql {
        public string SqlCreate () {
            return " INSERT INTO BolaoChat (UserId, Message, Date) " +
                "    VALUES (@UserId, @Message, GETDATE())";
        }

        public string SqlList () {
            return " SELECT " +
                " 	     BolaoUser.UserId  " +
                "       ,BolaoUser.Name as UserName " +
                "       ,BolaoChat.Message " +
                " FROM BolaoChat " +
                " INNER JOIN BolaoUser ON (BolaoChat.UserId = BolaoUser.Id)  " +
                " ORDER BY Date DESC";
        }
    }
}