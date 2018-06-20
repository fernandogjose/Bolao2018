import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BASE_URL_API } from './config.service';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(private http: HttpClient) { }

  listBet() {
    return this.http.get(`${BASE_URL_API}/report/bet`);
  }

  listBetByGame(oficialGameId: number) {
    return this.http.get(`${BASE_URL_API}/report/bet-by-game/${oficialGameId}`);
  }
}
