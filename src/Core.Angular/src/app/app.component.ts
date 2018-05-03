import { Component } from '@angular/core';
import { UserLocalstorage } from './localstorage/user.localstorage';
import { User } from './models/user.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  userLogged: User = null;

  constructor(private userLocalstorage: UserLocalstorage) { }

  ngOnInit() {
    this.userLogged = this.userLocalstorage.getUserLogged();
  }

  showContentWrapper() {
    return {
      "content-wrapper": this.userLogged != null && this.userLogged != undefined
    }
  }
}
