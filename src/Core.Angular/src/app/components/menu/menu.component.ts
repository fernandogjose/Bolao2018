import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user.model';
import { UserLocalstorage } from '../../localstorage/user.localstorage';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  userLogged: User = null;

  constructor(private userLocalstorage: UserLocalstorage) { }

  ngOnInit() {
    this.userLogged = this.userLocalstorage.getUserLogged();
  }
}
