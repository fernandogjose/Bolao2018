import { User } from "./user.model";
import { OficialGame } from "./oficial-game.model";

export class UserGame {
    public id: number;
    public UserId: number;
    public GameId: number;
    public ScoreTeamA: number;
    public ScoreTeamB: number;
    public Points: number;
    public User: User;
    public OficialGame: OficialGame;
}