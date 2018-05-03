import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from '../../models/user.model';
import { SharedService } from '../../services/shared.service';
import { UserService } from '../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ResponseApi } from '../../models/response-api';
import { UserLocalstorage } from '../../localstorage/user.localstorage';

@Component({
  selector: 'app-user-new',
  templateUrl: './user-new.component.html',
  styleUrls: ['./user-new.component.css']
})
export class UserNewComponent implements OnInit {

  @ViewChild("form")
  form: NgForm

  user = this.resetUser();
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

  private resetUser(): User {
    return new User(0, '', '', '', '', 0);
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

  ngOnInit() {
  }

  create() {
    this.loading = true;
    this.message = {};
    this.userService
      .create(this.user)
      .subscribe((userResponse: User) => {
        this.user = this.resetUser();
        this.form.resetForm();
        this.showMessage({
          type: 'success',
          text: 'Boa parça!!! seu usuário foi criado. Segura ai que vou te levar para o jogo'
        });

        //--- envia para a home
        setTimeout(() => {
          this.loading = false;
          this.shared.showTemplate.emit(true)
          this.shared.user = userResponse;
          this.userLocalstorage.setUserLogged(userResponse);
          this.router.navigate(['/classificacao']);
        }, 8000);
      }, err => {
        this.showMessage({
          type: 'error',
          text: err.error.error
        });

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
