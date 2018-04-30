import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from '../../services/shared.service';
import { UserPointClassification } from '../../models/user-point-classification.model';
import { HomeService } from '../../services/home.service';
import { ChatService } from '../../services/chat.service';
import { Chat } from '../../models/chat.model';
import { NgForm } from '@angular/forms';
import { ChatCreateRequest } from '../../models/chat-create-request.model';
import { UserLocalstorage } from '../../localstorage/user.localstorage';

@Component({
  selector: 'app-classification',
  templateUrl: './classification.component.html',
  styleUrls: ['./classification.component.css']
})
export class ClassificationComponent implements OnInit {

  @ViewChild("form")
  form: NgForm

  loading: boolean;
  shared: SharedService;
  userPointClassifications: UserPointClassification[];
  chats: Chat[];
  chatCreateRequest = this.resetChatCreateRequest();

  constructor(private chatService: ChatService, private homeService: HomeService, private router: Router, private userLocalstorage: UserLocalstorage) {
    this.shared = SharedService.getInstance();
  }

  private resetChatCreateRequest(): ChatCreateRequest {
    return new ChatCreateRequest(0, '');
  }

  ngOnInit() {
    this.classificationList();
    this.chatList();
  }

  classificationList() {
    this.homeService
      .list()
      .subscribe((userPointClassifications: UserPointClassification[]) => {
        this.userPointClassifications = userPointClassifications;
        this.getMyPosition();
      }, error => {
        if (error.status == 401) {
          this.shared.showTemplate.emit(false);
          this.shared.user = null;
          this.router.navigate(['/login']);
        }
      });
  }

  chatList() {
    this.chatService
      .list()
      .subscribe((chats: Chat[]) => {
        this.chats = chats;
      }, error => {
        if (error.status == 401) {
          this.shared.showTemplate.emit(false);
          this.shared.user = null;
          this.router.navigate(['/login']);
        }
      });
  }

  chatCreate() {
    this.loading = true;
    this.chatCreateRequest.userId = this.shared.user.id;

    this.chatService
      .create(this.chatCreateRequest)
      .subscribe(() => {
        this.loading = false;
        this.form.resetForm();
        this.chatCreateRequest = this.resetChatCreateRequest();
        this.chatList();
      }, error => {
        if (error.status == 401) {
          this.shared.showTemplate.emit(false);
          this.shared.user = null;
          this.router.navigate(['/login']);
          this.loading = false;
        }
      });
  }

  private getMyPosition(): void {
    for (let index = 0; index < this.userPointClassifications.length; index++) {
      if (this.userPointClassifications[index].userId == this.shared.user.id) {
        this.shared.user.position = this.userPointClassifications[index].position;
        this.userLocalstorage.setUserLogged(this.shared.user);
        return;
      }
    }
  }
}