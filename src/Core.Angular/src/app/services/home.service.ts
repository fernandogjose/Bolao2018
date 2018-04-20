import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL_API } from './config.service';

@Injectable()
export class HomeService {

  constructor(private http: HttpClient) { }

  list() {
    return this.http.get(`${BASE_URL_API}/userpoint`);
  }

}
