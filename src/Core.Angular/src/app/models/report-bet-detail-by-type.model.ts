import { ReportBetDetail } from "./report-bet-detail.model";

export class ReportBetDetailByType {
    public oficialGameId: number;
    public oficialGameDate: Date;
    public groupName: string;
    public teamAName: string;
    public teamBName: string;
    public winsTeamA: ReportBetDetail[];
    public winsTeamB:ReportBetDetail[];
    public draws:ReportBetDetail[];
}