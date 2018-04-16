import { Component, OnInit } from '@angular/core';
import { UserGameService } from '../../services/user-game.service';
import { SharedService } from '../../services/shared.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ResponseApi } from '../../models/response-api';
import { UserGame } from '../../models/user-game.model';

@Component({
  selector: 'app-user-game',
  templateUrl: './user-game.component.html',
  styleUrls: ['./user-game.component.css']
})
export class UserGameComponent implements OnInit {

  shared: SharedService;
  message: {};
  classCss: {};
  userGames: UserGame[];

  constructor(private userGameService: UserGameService,
    private activatedRoute: ActivatedRoute,
    private router: Router) {
    this.shared = SharedService.getInstance();
  }

  ngOnInit() {
    let userId: number = this.activatedRoute.snapshot.params['userId'];
    if (userId != undefined) {
      this.getByUserId(userId);
    }
  }

  private showMessage(message: { type: string, text: string }): void {
    this.message = message;
    this.buildClasses(message.type);

    setTimeout(() => {
      this.message = undefined;
    }, 10000);
  }

  private buildClasses(type: string): void {
    this.classCss = {
      'alert': true
    }

    this.classCss['alert-' + type] = true;
  }

  getByUserId(userId: number) {
    this.userGameService
      .listByUserId(userId)
      .subscribe((userGames: UserGame[]) => {
        this.userGames = userGames;
      }, err => {
        if (err.status == 401) {
          this.shared.showTemplate.emit(false);
          this.shared.user = null;
          this.router.navigate(['/login']);
        }

        this.showMessage({
          type: 'error',
          text: err.error.error
        });
      });
  }

}
