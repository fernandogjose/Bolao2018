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

    let gamesByGroupMock = '[{"groupName":"Grupo A","games":[{"oficialGameId":1,"gameDate":"2018-06-14T12:00:00","teamA":"Rússia","teamB":"Arábia Saudita","groupName":"Grupo A","scoreTeamA":5,"scoreTeamB":4,"canSave":true},{"oficialGameId":3,"gameDate":"2018-06-15T09:00:00","teamA":"Egito","teamB":"Uruguai","groupName":"Grupo A","scoreTeamA":3,"scoreTeamB":3,"canSave":true},{"oficialGameId":4,"gameDate":"2018-06-19T15:00:00","teamA":"Rússia","teamB":"Egito","groupName":"Grupo A","scoreTeamA":1,"scoreTeamB":1,"canSave":true},{"oficialGameId":5,"gameDate":"2018-06-20T12:00:00","teamA":"Uruguai","teamB":"Arábia Saudita","groupName":"Grupo A","scoreTeamA":2,"scoreTeamB":2,"canSave":true},{"oficialGameId":7,"gameDate":"2018-06-25T11:00:00","teamA":"Arábia Saudita","teamB":"Egito","groupName":"Grupo A","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":6,"gameDate":"2018-06-25T12:00:00","teamA":"Uruguai","teamB":"Rússia","groupName":"Grupo A","scoreTeamA":0,"scoreTeamB":0,"canSave":true}]},{"groupName":"Grupo B","games":[{"oficialGameId":2,"gameDate":"2018-06-15T12:00:00","teamA":"Marrocos","teamB":"Irã","groupName":"Grupo B","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":8,"gameDate":"2018-06-15T15:00:00","teamA":"Portugal","teamB":"Espanha","groupName":"Grupo B","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":9,"gameDate":"2018-06-20T09:00:00","teamA":"Portugal","teamB":"Marrocos","groupName":"Grupo B","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":10,"gameDate":"2018-06-20T15:00:00","teamA":"Irã","teamB":"Espanha","groupName":"Grupo B","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":11,"gameDate":"2018-06-25T15:00:00","teamA":"Espanha","teamB":"Marrocos","groupName":"Grupo B","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":12,"gameDate":"2018-06-25T15:00:00","teamA":"Irã","teamB":"Portugal","groupName":"Grupo B","scoreTeamA":0,"scoreTeamB":0,"canSave":true}]},{"groupName":"Grupo C","games":[{"oficialGameId":13,"gameDate":"2018-06-16T07:00:00","teamA":"França","teamB":"Austrália","groupName":"Grupo C","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":14,"gameDate":"2018-06-16T13:00:00","teamA":"Peru","teamB":"Dinamarca","groupName":"Grupo C","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":16,"gameDate":"2018-06-21T09:00:00","teamA":"França","teamB":"Peru","groupName":"Grupo C","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":15,"gameDate":"2018-06-21T12:00:00","teamA":"Dinamarca","teamB":"Austrália","groupName":"Grupo C","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":17,"gameDate":"2018-06-26T11:00:00","teamA":"Dinamarca","teamB":"França","groupName":"Grupo C","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":18,"gameDate":"2018-06-26T11:00:00","teamA":"Austrália","teamB":"Peru","groupName":"Grupo C","scoreTeamA":0,"scoreTeamB":0,"canSave":true}]},{"groupName":"Grupo D","games":[{"oficialGameId":19,"gameDate":"2018-06-16T10:00:00","teamA":"Argentina","teamB":"Islândia","groupName":"Grupo D","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":20,"gameDate":"2018-06-16T16:00:00","teamA":"Croácia","teamB":"Nigéria","groupName":"Grupo D","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":21,"gameDate":"2018-06-21T15:00:00","teamA":"Argentina","teamB":"Croácia","groupName":"Grupo D","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":22,"gameDate":"2018-06-22T12:00:00","teamA":"Nigéria","teamB":"Islândia","groupName":"Grupo D","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":23,"gameDate":"2018-06-26T15:00:00","teamA":"Islândia","teamB":"Croácia","groupName":"Grupo D","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":24,"gameDate":"2018-06-26T15:00:00","teamA":"Nigéria","teamB":"Argentina","groupName":"Grupo D","scoreTeamA":0,"scoreTeamB":0,"canSave":true}]},{"groupName":"Grupo E","games":[{"oficialGameId":25,"gameDate":"2018-06-17T09:00:00","teamA":"Costa Rica","teamB":"Sérvia","groupName":"Grupo E","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":26,"gameDate":"2018-06-17T15:00:00","teamA":"Brasil","teamB":"Suiça","groupName":"Grupo E","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":27,"gameDate":"2018-06-22T09:00:00","teamA":"Brasil","teamB":"Costa Rica","groupName":"Grupo E","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":28,"gameDate":"2018-06-22T15:00:00","teamA":"Sérvia","teamB":"Suiça","groupName":"Grupo E","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":29,"gameDate":"2018-06-27T15:00:00","teamA":"Sérvia","teamB":"Brasil","groupName":"Grupo E","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":30,"gameDate":"2018-06-27T15:00:00","teamA":"Suiça","teamB":"Costa Rica","groupName":"Grupo E","scoreTeamA":0,"scoreTeamB":0,"canSave":true}]},{"groupName":"Grupo F","games":[{"oficialGameId":31,"gameDate":"2018-06-17T12:00:00","teamA":"Alemanha","teamB":"México","groupName":"Grupo F","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":32,"gameDate":"2018-06-18T09:00:00","teamA":"Suécia","teamB":"Coreia do Sul","groupName":"Grupo F","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":33,"gameDate":"2018-06-23T12:00:00","teamA":"Alemanha","teamB":"Suécia","groupName":"Grupo F","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":34,"gameDate":"2018-06-23T15:00:00","teamA":"Coreia do Sul","teamB":"México","groupName":"Grupo F","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":35,"gameDate":"2018-06-27T11:00:00","teamA":"México","teamB":"Suécia","groupName":"Grupo F","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":36,"gameDate":"2018-06-27T11:00:00","teamA":"Coreia do Sul","teamB":"Alemanha","groupName":"Grupo F","scoreTeamA":0,"scoreTeamB":0,"canSave":true}]},{"groupName":"Grupo G","games":[{"oficialGameId":37,"gameDate":"2018-06-18T12:00:00","teamA":"Bélgica","teamB":"Panamá","groupName":"Grupo G","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":38,"gameDate":"2018-06-18T15:00:00","teamA":"Tunísia","teamB":"Inglaterra","groupName":"Grupo G","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":39,"gameDate":"2018-06-23T09:00:00","teamA":"Bélgica","teamB":"Tunísia","groupName":"Grupo G","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":40,"gameDate":"2018-06-24T09:00:00","teamA":"Inglaterra","teamB":"Panamá","groupName":"Grupo G","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":41,"gameDate":"2018-06-28T15:00:00","teamA":"Inglaterra","teamB":"Bélgica","groupName":"Grupo G","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":42,"gameDate":"2018-06-28T15:00:00","teamA":"Panamá","teamB":"Tunísia","groupName":"Grupo G","scoreTeamA":0,"scoreTeamB":0,"canSave":true}]},{"groupName":"Grupo H","games":[{"oficialGameId":43,"gameDate":"2018-06-19T09:00:00","teamA":"Polônia","teamB":"Senegal","groupName":"Grupo H","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":44,"gameDate":"2018-06-19T12:00:00","teamA":"Colômbia","teamB":"Japão","groupName":"Grupo H","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":45,"gameDate":"2018-06-24T12:00:00","teamA":"Japão","teamB":"Senegal","groupName":"Grupo H","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":46,"gameDate":"2018-06-24T15:00:00","teamA":"Polônia","teamB":"Colômbia","groupName":"Grupo H","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":47,"gameDate":"2018-06-28T11:00:00","teamA":"Senegal","teamB":"Colômbia","groupName":"Grupo H","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":48,"gameDate":"2018-06-28T11:00:00","teamA":"Japão","teamB":"Polônia","groupName":"Grupo H","scoreTeamA":0,"scoreTeamB":0,"canSave":true}]},{"groupName":"Oitavas de final","games":[{"oficialGameId":50,"gameDate":"2018-06-30T11:00:00","teamA":"1º C","teamB":"2º D","groupName":"Oitavas de final","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":49,"gameDate":"2018-06-30T15:00:00","teamA":"1º A","teamB":"2º B","groupName":"Oitavas de final","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":53,"gameDate":"2018-07-01T11:00:00","teamA":"1º B","teamB":"2º A","groupName":"Oitavas de final","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":54,"gameDate":"2018-07-01T15:00:00","teamA":"1º D","teamB":"2º C","groupName":"Oitavas de final","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":51,"gameDate":"2018-07-02T11:00:00","teamA":"1º E","teamB":"2º F","groupName":"Oitavas de final","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":52,"gameDate":"2018-07-02T15:00:00","teamA":"1º G","teamB":"2º H","groupName":"Oitavas de final","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":55,"gameDate":"2018-07-03T11:00:00","teamA":"1º F","teamB":"2º E","groupName":"Oitavas de final","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":56,"gameDate":"2018-07-03T15:00:00","teamA":"1º H","teamB":"2º G","groupName":"Oitavas de final","scoreTeamA":0,"scoreTeamB":0,"canSave":true}]},{"groupName":"Quartas de final","games":[{"oficialGameId":57,"gameDate":"2018-07-06T11:00:00","teamA":"Vencedor 1A x 2B","teamB":"Vencedor 1C x 2D","groupName":"Quartas de final","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":58,"gameDate":"2018-07-06T15:00:00","teamA":"Vencedor 1E x 2F","teamB":"Vencedor 1G x 2H","groupName":"Quartas de final","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":60,"gameDate":"2018-07-07T11:00:00","teamA":"Vencedor 1F x 2E","teamB":"Vencedor 1H x 2G","groupName":"Quartas de final","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":59,"gameDate":"2018-07-07T15:00:00","teamA":"Vencedor 1B x 2A","teamB":"Vencedor 1D x 2C","groupName":"Quartas de final","scoreTeamA":0,"scoreTeamB":0,"canSave":true}]},{"groupName":"Semifinal","games":[{"oficialGameId":61,"gameDate":"2018-07-10T15:00:00","teamA":"Vencedor Quartas 1","teamB":"Vencedor Quartas 2","groupName":"Semifinal","scoreTeamA":0,"scoreTeamB":0,"canSave":true},{"oficialGameId":62,"gameDate":"2018-07-11T15:00:00","teamA":"Vencedor Quartas 3","teamB":"Vencedor Quartas 4","groupName":"Semifinal","scoreTeamA":0,"scoreTeamB":0,"canSave":true}]},{"groupName":"3º lugar","games":[{"oficialGameId":64,"gameDate":"2018-07-14T11:00:00","teamA":"Perdedor Semifinal 1","teamB":"Perdedor Semifinal 2","groupName":"3º lugar","scoreTeamA":0,"scoreTeamB":0,"canSave":true}]},{"groupName":"Final","games":[{"oficialGameId":63,"gameDate":"2018-07-15T12:00:00","teamA":"Vencedor Semifinal 1","teamB":"Vencedor Semifinal 2","groupName":"Final","scoreTeamA":3,"scoreTeamB":4,"canSave":true}]}]';
    this.gamesByGroup = JSON.parse(gamesByGroupMock);
    return;

    // this.userGameService
    //   .listByUserId(userId)
    //   .subscribe((GamesByGroup: GameByGroup[]) => {
    //     this.gamesByGroup = GamesByGroup;
    //     console.log(JSON.stringify(GamesByGroup));
    //   }, error => {
    //     if (error.status == 401) {
    //       this.shared.showTemplate.emit(false);
    //       this.shared.user = null;
    //       this.router.navigate(['/login']);
    //     }
    //   });
  }

  save(indexGroup: number, indexGame: number): void {

    this.isLoading = true;

    var userGame = this.gamesByGroup[indexGroup].games[indexGame];
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
