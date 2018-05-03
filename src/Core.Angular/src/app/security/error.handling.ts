import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { UserLocalstorage } from '../localstorage/user.localstorage';

@Injectable()
export class ErrorHandling {

  constructor(private router: Router, private userLocalstorage: UserLocalstorage) { }

  handle(status: number) {
    if (status == 401) {
      this.userLocalstorage.removeUserLogged();
      this.router.navigate(['/login']);
    }
  }

}
