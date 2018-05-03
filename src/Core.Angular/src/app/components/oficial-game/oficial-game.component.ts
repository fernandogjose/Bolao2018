import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../services/shared.service';
import { GameByGroup } from '../../models/game-by-group.model';
import { ActivatedRoute, Router } from '@angular/router';
import { OficialGameService } from '../../services/oficial-game.service';

@Component({
  selector: 'app-oficial-game',
  templateUrl: './oficial-game.component.html',
  styleUrls: ['./oficial-game.component.css']
})
export class OficialGameComponent implements OnInit {

  shared: SharedService;
  message: {};
  classCss: {};
  gamesByGroup: GameByGroup[];
  isLoading: boolean;
  indexGroup: number;
  indexGame: number;

  constructor(private oficialGameService: OficialGameService, private router: Router) {
    this.shared = SharedService.getInstance();
  }

  ngOnInit() {
    this.list();
  }

  list() {
    this.oficialGameService
      .list()
      .subscribe((gamesByGroup: GameByGroup[]) => {
        this.gamesByGroup = gamesByGroup;
      }, error => {
        if (error.status == 401) {
          this.shared.showTemplate.emit(false);
          this.shared.user = null;
          this.router.navigate(['/login']);
        }
      });
  }

  updateScore(indexGroup: number, indexGame: number): void {
    this.isLoading = true;

    //--- salva os index
    this.indexGroup = indexGroup;
    this.indexGame = indexGame;

    //--- obter o jogo no array
    var game = this.gamesByGroup[indexGroup].games[indexGame];

    //--- verifica se os dados estão válidos
    if (game.scoreTeamA == null || game.scoreTeamA < 0 || game.scoreTeamB == null || game.scoreTeamB < 0) {
      this.isLoading = false;
      return;
    }

    //--- envia para a api atualizar o placar
    this.oficialGameService.updateScore(game).subscribe(
      success => {
        this.isLoading = false;
        this.gamesByGroup[this.indexGroup].games[this.indexGame].isCreateScore = false;
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

  deleteScore(indexGroup: number, indexGame: number): void {
    this.isLoading = true;

    //--- salva os index
    this.indexGroup = indexGroup;
    this.indexGame = indexGame;

    //--- obter o jogo no array
    var game = this.gamesByGroup[indexGroup].games[indexGame];

    //--- envia para a api salvar
    this.oficialGameService.deleteScore(game.oficialGameId).subscribe(
      success => {
        this.isLoading = false;
        this.gamesByGroup[this.indexGroup].games[this.indexGame].isCreateScore = true;
        this.gamesByGroup[this.indexGroup].games[this.indexGame].scoreTeamA = 0;
        this.gamesByGroup[this.indexGroup].games[this.indexGame].scoreTeamB = 0;
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
