import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BASE_URL_API } from './config.service';
import { UserGame } from '../models/user-game.model';

@Injectable()
export class UserGameService {

  constructor(private http: HttpClient) { }

  listByUserId(userId: number) {
    return this.http.get(`${BASE_URL_API}/usergame/listbyuserid/${userId}`);
  }

  save(userGame: UserGame) {
    // var userGameSaveRequest = new UserGameSaveRequest(
    //   userGame.userId,
    //   userGame.oficialGameId,
    //   userGame.scoreTeamA,
    //   userGame.scoreTeamB,
    //   userGame.gameDate
    // );

    var teste = {
      'userId': userGame.userId,
      'oficialGame': {
        'date': Date.parse(userGame.gameDate)
      }
    };

    return this.http.post(`${BASE_URL_API}/usergame`, userGame);
  }

}
