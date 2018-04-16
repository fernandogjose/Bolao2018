import { Team } from "./team.model";
import { Group } from "./group.model";

export class OficialGame{
    public Id: number;
    public GroupId: number;
    public TeamAId: number;
    public TeamBId: number;
    public ScoreTeamA: number;
    public ScoreTeamB: number;
    public Date: string;
    public Group: Group;
    public TeamA: Team;
    public TeamB: Team;
}