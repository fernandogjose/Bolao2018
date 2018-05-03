import { Component, OnInit } from '@angular/core';
import { UserGameService } from '../../services/user-game.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ResponseApi } from '../../models/response-api';
import { GameByGroup } from '../../models/game-by-group.model';
import { UserLocalstorage } from '../../localstorage/user.localstorage';
import { ErrorHandling } from '../../security/error.handling';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-user-game',
  templateUrl: './user-game.component.html',
  styleUrls: ['./user-game.component.css']
})
export class UserGameComponent implements OnInit {

  message: {};
  classCss: {};
  gamesByGroup: GameByGroup[];
  isLoading: boolean;
  userName: string;
  userLogged: User = null;

  constructor(private userGameService: UserGameService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private userLocalstorage: UserLocalstorage,
    private errorHandling: ErrorHandling) { }

  ngOnInit() {
    this.userLogged = this.userLocalstorage.getUserLogged();
    
    var userId: number = this.activatedRoute.snapshot.params['userId'];
    if (userId == undefined) {
      userId = this.userLogged.id;
    }

    this.getByUserId(userId);
  }

  getByUserId(userId: number) {

    this.userGameService
      .listByUserId(userId)
      .subscribe((gamesByGroup: GameByGroup[]) => {
        this.gamesByGroup = gamesByGroup;
        this.userName = gamesByGroup[0].games[0].userName;
      }, error => {
        this.errorHandling.handle(error.status);
      });
  }

  save(indexGroup: number, indexGame: number): void {

    this.isLoading = true;

    var userGame = this.gamesByGroup[indexGroup].games[indexGame];
    if (userGame.scoreTeamA == null || userGame.scoreTeamA < 0 || userGame.scoreTeamB == null || userGame.scoreTeamB < 0) {
      this.isLoading = false;
      return;
    }

    var userLogged = this.userLocalstorage.getUserLogged();
    userGame.userId = userLogged.id;
    this.userGameService.save(userGame).subscribe(
      success => {
        this.isLoading = false;
      },
      error => {
        this.errorHandling.handle(error.status);
      });

  }
}
