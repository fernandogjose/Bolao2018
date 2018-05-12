import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../../models/user.model';
import { SharedService } from '../../services/shared.service';
import { UserService } from '../../services/user.service';
import { UserLocalstorage } from '../../localstorage/user.localstorage';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  user = new User(0, '', '', '', '', 0);
  shared: SharedService;
  message: {};
  classCss: {};
  loading: boolean;

  constructor(
    private userService: UserService,
    private router: Router,
    private userLocalstorage: UserLocalstorage
  ) {
    this.shared = SharedService.getInstance();
  }

  ngOnInit() {
    this.shared.showTemplate.emit(false);
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

  login() {
    this.loading = true;
    this.message = '';
    this.userService
      .login(this.user)
      .subscribe((userAuthentication: User) => {
        this.shared.user = userAuthentication;
        this.userLocalstorage.setUserLogged(userAuthentication);
        this.loading = false;
        this.router.navigate(['/classificacao']);
      }, err => {
        this.shared.user = null;
        this.showMessage({
          type: 'error',
          text: err.error.error
        });
        this.shared.showTemplate.emit(false);
        this.loading = false;
      });
  }

  getFromGroupClass(isInvalid: boolean, isDirty): {} {
    return {
      'form-group': true,
      'has-error': isInvalid && isDirty,
      'has-default': !isInvalid && isDirty
    }
  }

}
