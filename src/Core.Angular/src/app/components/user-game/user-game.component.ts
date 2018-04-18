import { Component, OnInit } from '@angular/core';
import { UserGameService } from '../../services/user-game.service';
import { SharedService } from '../../services/shared.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ResponseApi } from '../../models/response-api';
import { UserGameByGroup } from '../../models/user-game-by-group.model';

@Component({
  selector: 'app-user-game',
  templateUrl: './user-game.component.html',
  styleUrls: ['./user-game.component.css']
})
export class UserGameComponent implements OnInit {

  shared: SharedService;
  message: {};
  classCss: {};
  userGamesByGroup: UserGameByGroup[];
  isLoading: boolean;

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

    // let userGamesByGroupMock = '[{"groupName":"A","userGames":[{"oficialGameId":1,"gameDate":"14/06/2018 12:00","teamA":"Rússia","teamB":"Arábia Saudita","groupName":"A","scoreTeamA":0,"scoreTeamB":0},{"oficialGameId":3,"gameDate":"15/06/2018 09:00","teamA":"Egito","teamB":"Uruguai","groupName":"A","scoreTeamA":0,"scoreTeamB":0},{"oficialGameId":4,"gameDate":"19/06/2018 15:00","teamA":"Rússia","teamB":"Egito","groupName":"A","scoreTeamA":0,"scoreTeamB":0},{"oficialGameId":5,"gameDate":"20/06/2018 12:00","teamA":"Uruguai","teamB":"Arábia Saudita","groupName":"A","scoreTeamA":0,"scoreTeamB":0},{"oficialGameId":7,"gameDate":"25/06/2018 11:00","teamA":"Arábia Saudita","teamB":"Egito","groupName":"A","scoreTeamA":0,"scoreTeamB":0},{"oficialGameId":6,"gameDate":"25/06/2018 12:00","teamA":"Uruguai","teamB":"Rússia","groupName":"A","scoreTeamA":0,"scoreTeamB":0}]},{"groupName":"B","userGames":[{"oficialGameId":2,"gameDate":"15/06/2018 12:00","teamA":"Marrocos","teamB":"Irã","groupName":"B","scoreTeamA":0,"scoreTeamB":0}]}]';
    // this.userGamesByGroup = JSON.parse(userGamesByGroupMock);
    // return;

    this.userGameService
      .listByUserId(userId)
      .subscribe((userGamesByGroup: UserGameByGroup[]) => {
        this.userGamesByGroup = userGamesByGroup;
        var teste = JSON.stringify(userGamesByGroup);
        var teste1 = JSON.parse(teste);
      }, error => {
        if (error.status == 401) {
          this.shared.showTemplate.emit(false);
          this.shared.user = null;
          this.router.navigate(['/login']);
        }

        this.showMessage({
          type: 'error',
          text: error.error.error
        });
      });
  }

  save(indexGroup: number, indexUserGame: number): void {

    this.isLoading = true;

    var userGame = this.userGamesByGroup[indexGroup].userGames[indexUserGame];
    if (userGame.scoreTeamA == null || userGame.scoreTeamA < 0 || userGame.scoreTeamB == null || userGame.scoreTeamB < 0) {
      this.isLoading = false;
      return;
    }

    userGame.userId = this.shared.user.id;
    this.userGameService.save(userGame).subscribe(success => {
      this.isLoading = false;
    }, error => {
      if (error.status == 401) {
        this.shared.showTemplate.emit(false);
        this.shared.user = null;
        this.router.navigate(['/login']);

        this.isLoading = false;
      }
    });

  }
}
