import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BASE_URL_API } from './config.service';
import { Game } from '../models/game.model';

@Injectable()
export class UserGameService {

  constructor(private http: HttpClient) { }

  listByUserId(userId: number) {
    return this.http.get(`${BASE_URL_API}/usergame/listbyuserid/${userId}`);
  }

  save(game: Game) {
    return this.http.post(`${BASE_URL_API}/usergame`, game);
  }

}
