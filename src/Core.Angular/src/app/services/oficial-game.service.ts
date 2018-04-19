import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BASE_URL_API } from './config.service';
import { Game } from '../models/game.model';

@Injectable()
export class OficialGameService {

  constructor(private http: HttpClient) { }

  list() {
    return this.http.get(`${BASE_URL_API}/oficialgame`);
  }

  updateScore(game: Game) {
    return this.http.put(`${BASE_URL_API}/oficialgame`, game);
  }

  deleteScore(oficialGameId: number) {
    return this.http.delete(BASE_URL_API + '/oficialgame/' + oficialGameId);
  }

}
