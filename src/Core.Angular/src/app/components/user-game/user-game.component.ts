import { Component, OnInit, Inject } from '@angular/core';
import { UserGameService } from '../../services/user-game.service';
import { SharedService } from '../../services/shared.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ResponseApi } from '../../models/response-api';
import { GameByGroup } from '../../models/game-by-group.model';
import { UserLocalstorage } from '../../localstorage/user.localstorage';
import { ErrorInterceptor } from '../../security/error.interceptor';

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
  userId: number;
  showSuccess: boolean = false;
  showError: boolean = false;

  constructor(private userGameService: UserGameService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private userLocalstorage: UserLocalstorage,
    private errorInteceptor: ErrorInterceptor) {
    this.shared = SharedService.getInstance();
  }

  ngOnInit() {
    let userId: number = this.activatedRoute.snapshot.params['userId'];
    if (userId == undefined) {
      userId = this.userLocalstorage.getUserLogged().id;
    } else {
      // this.router.navigate(['/hoje-nao']);
    }

    this.getByUserId(userId);
  }

  getByUserId(userId: number) {

    this.userGameService
      .listByUserId(userId)
      .subscribe((gamesByGroup: GameByGroup[]) => {
        this.gamesByGroup = gamesByGroup;
        this.userName = gamesByGroup[0].games[0].userName;
        this.userId = gamesByGroup[0].games[0].userId;
      }, error => {
        this.errorInteceptor.get(error.status);
      });
  }

  save(): void {

    this.isLoading = true;
    this.showSuccess = false;
    this.showError = false;

    this.userGameService.save(this.gamesByGroup).subscribe(
      success => {
        this.isLoading = false;
        this.showSuccess = true;
      },
      error => {
        this.errorInteceptor.get(error.status);
        this.isLoading = false;
        this.showError = true;
      });

  }
}
