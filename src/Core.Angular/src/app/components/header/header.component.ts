import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user.model';
import { UserLocalstorage } from '../../localstorage/user.localstorage';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private userLocalstorage: UserLocalstorage) { }

  ngOnInit() {

  }

  signOut(): void {
    this.userLocalstorage.removeUserLogged();
    window.location.href = '/login';
  }
}
