import { Component, OnInit } from '@angular/core';
import { UserGameService } from '../../services/user-game.service';
import { SharedService } from '../../services/shared.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ResponseApi } from '../../models/response-api';
import { GameByGroup } from '../../models/game-by-group.model';

@Component({
  selector: 'app-user-game',
  templateUrl: './user-game.component.html',
  styleUrls: ['./user-game.component.css']
})
export class UserGameComponent implements OnInit {

  shared: SharedService;
  message: {};
  classCss: {};
  gamesByGroup: GameByGroup[];
  isLoading: boolean;
  userName: string;

  constructor(private userGameService: UserGameService,
    private activatedRoute: ActivatedRoute,
    private router: Router) {
    this.shared = SharedService.getInstance();
  }

  ngOnInit() {
    let userId: number = this.activatedRoute.snapshot.params['userId'];
    if (userId == undefined) {
      userId = this.shared.user.id;
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
        if (error.status == 401) {
          this.shared.showTemplate.emit(false);
          this.shared.user = null;
          this.router.navigate(['/login']);
        }
      });
  }

  save(indexGroup: number, indexGame: number): void {

    this.isLoading = true;

    var userGame = this.gamesByGroup[indexGroup].games[indexGame];
    if (userGame.scoreTeamA == null || userGame.scoreTeamA < 0 || userGame.scoreTeamB == null || userGame.scoreTeamB < 0) {
      this.isLoading = false;
      return;
    }

    userGame.userId = this.shared.user.id;
    this.userGameService.save(userGame).subscribe(
      success => {
        this.isLoading = false;
      },
      error => {
        if (error.status == 401) {
          this.shared.showTemplate.emit(false);
          this.shared.user = null;
          this.router.navigate(['/login']);
          this.isLoading = false;
        }
      });

  }
}
