export class UserGameByGroup {
    public groupName: string;
    public games: Game[];
}

export class Game {
    public oficialGameId: number;
    public gameDate: string;
    public teamA: string;
    public teamB: string;
    public scoreTeamA: number;
    public scoreTeamB: number;
}