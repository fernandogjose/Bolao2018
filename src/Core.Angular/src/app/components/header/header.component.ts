import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../services/shared.service';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  shared: SharedService;

  constructor() {
    this.shared = SharedService.getInstance();
  }

  ngOnInit() {

  }

  signOut() : void {
    localStorage.removeItem("userLoggedLocalStorage");
    this.shared.user = null;
    window.location.href = '/';
  }
}
