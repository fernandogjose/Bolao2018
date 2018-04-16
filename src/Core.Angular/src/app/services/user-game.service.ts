import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BASE_URL_API } from './config.service';

@Injectable()
export class UserGameService {

  constructor(private http: HttpClient) { }

  listByUserId(userId: number){
    const headers = new HttpHeaders({'teste':'application/json; charset=utf-8'});
    return this.http.get(`${BASE_URL_API}/usergame/listbyuserid/${userId}`);
  }

}
