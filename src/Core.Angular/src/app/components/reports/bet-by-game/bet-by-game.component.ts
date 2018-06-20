import { Component, OnInit } from '@angular/core';
import { ReportService } from '../../../services/report.service';
import { ReportBet } from '../../../models/report-bet.model';
import { ErrorInterceptor } from '../../../security/error.interceptor';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ReportBetDetailByType } from '../../../models/report-bet-detail-by-type.model';

@Component({
  selector: 'app-bet-by-game',
  templateUrl: './bet-by-game.component.html',
  styleUrls: ['./bet-by-game.component.css']
})
export class BetByGameComponent implements OnInit {

  reportBetDetailByType: ReportBetDetailByType;

  constructor(
    private reportService: ReportService,
    private errorInteceptor: ErrorInterceptor,
    private activatedRoute: ActivatedRoute,
    private router: Router,
  ) { }

  ngOnInit() {
    let oficialGameId: number = this.activatedRoute.snapshot.params['oficialGameId'];
    if (oficialGameId == undefined) {
      this.router.navigate(['/classificacao']);
    }
    
    this.listByGame(oficialGameId);
  }

  listByGame(oficialGameId: number) {
    this.reportService
      .listBetByGame(oficialGameId)
      .subscribe((reportBetDetailByType: ReportBetDetailByType) => {
        this.reportBetDetailByType = reportBetDetailByType;
      }, error => {
        this.errorInteceptor.get(error.status);
      });
  }

}
