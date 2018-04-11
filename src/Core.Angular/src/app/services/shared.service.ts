import { Injectable, EventEmitter } from '@angular/core';
import { User } from '../models/user.model';

@Injectable()
export class SharedService {

  public static instance: SharedService = null;
  user: User;
  token: string;
  showTemplate = new EventEmitter<boolean>();

  constructor() {
    return SharedService.instance = SharedService.instance || this;
  }

  public static getInstance() {
    if (this.instance == null) {
      this.instance = new SharedService();
    }

    return this.instance;
  }

  isLoggedIn(): boolean {
    return this.user != null && this.user.email != '';
  }
}
