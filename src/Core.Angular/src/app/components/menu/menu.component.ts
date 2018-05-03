import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../services/shared.service';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  public shared: SharedService;

  constructor() { 
    this.shared = SharedService.getInstance();
    this.shared.user = new User(0, '','','','', 0);
  }

  ngOnInit() {
  }

}
