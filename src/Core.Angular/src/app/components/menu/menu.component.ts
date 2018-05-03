import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user.model';
import { SharedService } from '../../services/shared.service';
import { UserLocalstorage } from '../../localstorage/user.localstorage';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  sharedService: SharedService;
  userLogged: User = null;

  constructor(private userLocalstorage: UserLocalstorage) {
    this.sharedService = SharedService.getInstance();
  }

  ngOnInit() {
    this.sharedService.userLogged.subscribe(
      userLogged => this.userLogged = userLogged
    );

    if (this.userLogged == null || this.userLogged == undefined) {
      this.userLogged = this.userLocalstorage.getUserLogged();
    }
  }
}
