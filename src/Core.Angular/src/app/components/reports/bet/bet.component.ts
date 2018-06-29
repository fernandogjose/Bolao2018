import { Component, OnInit } from '@angular/core';
import { ReportService } from '../../../services/report.service';
import { ReportBet } from '../../../models/report-bet.model';
import { ErrorInterceptor } from '../../../security/error.interceptor';
import { Router } from '@angular/router';

@Component({
  selector: 'app-bet',
  templateUrl: './bet.component.html',
  styleUrls: ['./bet.component.css']
})
export class BetComponent implements OnInit {

  reportBets: ReportBet[]

  constructor(private reportService: ReportService, 
              private errorInteceptor: ErrorInterceptor,
              private router: Router) { }

  ngOnInit() {
    this.router.navigate(['/hoje-nao']);
    //this.list();
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
