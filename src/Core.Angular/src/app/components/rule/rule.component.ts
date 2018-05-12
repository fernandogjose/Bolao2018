import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../services/shared.service';
import { UserLocalstorage } from '../../localstorage/user.localstorage';

@Component({
  selector: 'app-rule',
  templateUrl: './rule.component.html',
  styleUrls: ['./rule.component.css']
})
export class RuleComponent implements OnInit {

  public shared: SharedService;

  constructor(private userLocalstorage: UserLocalstorage) {
  }

  ngOnInit() {
    this.shared = SharedService.getInstance();
    this.shared.showTemplate.emit(false);

    var userLoggedLocalStorage = this.userLocalstorage.getUserLogged();
    if (userLoggedLocalStorage != null) {
      this.shared.user = userLoggedLocalStorage;
      this.shared.showTemplate.emit(true);
    }
  }
}
