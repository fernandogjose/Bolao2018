import { UserService } from './user.service';
import { Injectable, EventEmitter } from '@angular/core';
import { User } from '../models/user.model';

@Injectable()
export class SharedService {

  public static instance: SharedService = null;
  userLogged = new EventEmitter<User>();

  constructor() {
    this.userLogged.emit(new User(0, 'teste', 'teste', 'teste', 'teste', 10));
    return SharedService.instance = SharedService.instance || this;
  }

  public static getInstance() {
    if (this.instance == null) {
      this.instance = new SharedService();
    }
    return this.instance;
  }
}