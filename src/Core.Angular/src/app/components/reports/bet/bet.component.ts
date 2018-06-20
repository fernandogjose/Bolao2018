import { Component, OnInit } from '@angular/core';
import { ReportService } from '../../../services/report.service';
import { ReportBet } from '../../../models/report-bet.model';
import { ErrorInterceptor } from '../../../security/error.interceptor';

@Component({
  selector: 'app-bet',
  templateUrl: './bet.component.html',
  styleUrls: ['./bet.component.css']
})
export class BetComponent implements OnInit {

  reportBets: ReportBet[]

  constructor(private reportService: ReportService, 
              private errorInteceptor: ErrorInterceptor) { }

  ngOnInit() {
    this.list();
  }

  list() {
    this.reportService
      .listBet()
      .subscribe((reportBets: ReportBet[]) => {
        this.reportBets = reportBets;
      }, error => {
        this.errorInteceptor.get(error.status);
      });
  }
}
