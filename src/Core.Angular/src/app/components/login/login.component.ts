import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../../models/user.model';
import { SharedService } from '../../services/shared.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  user = new User('0', '', '', '', '');
  shared: SharedService;
  message: {};
  classCss: {};

  constructor(
    private userService: UserService,
    private router: Router
  ) {
    this.shared = SharedService.getInstance();
  }

  ngOnInit() {
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
    this.message = '';
    this.userService
      .login(this.user)
      .subscribe((userAuthentication: User) => {
        this.shared.user = userAuthentication;
        this.shared.showTemplate.emit(true)
        this.router.navigate(['/']);
      }, err => {
        this.shared.user = null;
        this.showMessage({
          type: 'error',
          text: err.error.error
        });
        this.shared.showTemplate.emit(false);
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
