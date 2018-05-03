import { Component } from '@angular/core';
import { UserLocalstorage } from './localstorage/user.localstorage';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  showTemplate: boolean = false;
  isLoggedIn: boolean = false;

  constructor(private userLocalstorage: UserLocalstorage) { }

  ngOnInit() {
    var userLogged = this.userLocalstorage.getUserLogged();
    if (userLogged !== undefined) {
      this.showTemplate = true;
      this.isLoggedIn = true;
    }
  }

  showContentWrapper() {
    return {
      "content-wrapper": this.isLoggedIn
    }
  }
}
