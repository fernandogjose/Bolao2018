import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from '../../models/user.model';
import { SharedService } from '../../services/shared.service';
import { UserService } from '../../services/user.service';
import { ActivatedRoute } from '@angular/router';
import { ResponseApi } from '../../models/response-api';

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

  constructor(
    private userService: UserService,
    private route: ActivatedRoute
  ) {
    this.shared = SharedService.getInstance();
  }

  private resetUser(): User {
    return new User('0', '', '', '', '');
  }

  private showMessage(message: { type: string, text: string }): void {
    this.message = message;
    this.buildClasses(message.type);

    setTimeout(() => {
      this.message = undefined;
    }, 3000);
  }

  private buildClasses(type: string): void {
    this.classCss = {
      'alert': true
    }

    this.classCss['alert-' + type] = true;
  }

  ngOnInit() {
    let id: string = this.route.snapshot.params['id'];
    if (id != undefined) {
      this.get(id);
    }
  }

  get(id: string) {
    this.userService
      .get(id)
      .subscribe((responseApi: ResponseApi) => {
        this.user = responseApi.data;
        this.user.Password = '';
      }, err => {
        this.showMessage({
          type: 'error',
          text: err['error']['errors'][0]
        });
      });
  }

  create() {
    this.message = {};
    this.userService
      .create(this.user)
      .subscribe((responseApi: ResponseApi) => {
        this.user = this.resetUser();
        this.form.resetForm();
        this.showMessage({
          type: 'success',
          text: 'usuário cadastrado com sucesso'
        });
      }, err => {
        this.showMessage({
          type: 'error',
          text: err['error']['errors'][0]
        });
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
