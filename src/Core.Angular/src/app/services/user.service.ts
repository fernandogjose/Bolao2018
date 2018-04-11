import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user.model';
import { BASE_URL_API } from './config.service';

@Injectable()
export class UserService {

  constructor(private http: HttpClient) { }

  login(user: User) {
    return this.http.get(`${BASE_URL_API}/user/login/${user.email}/${user.password}`);
  }
}
