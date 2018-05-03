import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { UserPointClassification } from '../../models/user-point-classification.model';
import { HomeService } from '../../services/home.service';
import { ChatService } from '../../services/chat.service';
import { Chat } from '../../models/chat.model';
import { NgForm } from '@angular/forms';
import { ChatCreateRequest } from '../../models/chat-create-request.model';
import { UserLocalstorage } from '../../localstorage/user.localstorage';
import { ErrorHandling } from '../../security/error.handling';
import { SharedService } from '../../services/shared.service';

@Component({
  selector: 'app-classification',
  templateUrl: './classification.component.html',
  styleUrls: ['./classification.component.css']
})
export class ClassificationComponent implements OnInit {

  message: string;
  loading: boolean;
  showTemplate: boolean;
  userPointClassifications: UserPointClassification[];
  chats: Chat[];
  sharedService: SharedService;

  constructor(private chatService: ChatService,
    private homeService: HomeService,
    private router: Router,
    private userLocalstorage: UserLocalstorage,
    private errorHandling: ErrorHandling) {
      this.sharedService = SharedService.getInstance();
  }

  ngOnInit() {
    this.classificationList();
    this.chatList();
    this.loading = true;
  }

  classificationList() {
    this.homeService
      .list()
      .subscribe((userPointClassifications: UserPointClassification[]) => {
        this.userPointClassifications = userPointClassifications;
        this.getMyPosition();
        this.showTemplate = true;
        this.loading = false;
      }, error => {
        this.errorHandling.handle(error.status);
      });
  }

  chatList() {
    this.chatService
      .list()
      .subscribe((chats: Chat[]) => {
        this.chats = chats;
        this.showTemplate = true;
        this.loading = false;
      }, error => {
        this.errorHandling.handle(error.status);
      });
  }

  chatCreate() {
    this.loading = true;
    this.message = (document.getElementById("message") as HTMLTextAreaElement).value;

    if (this.message == '') {
      this.loading = false;
      return;
    }

    var userLogged = this.userLocalstorage.getUserLogged();
    var chatCreateRequest = new ChatCreateRequest(userLogged.id, this.message);

    this.chatService
      .create(chatCreateRequest)
      .subscribe(() => {
        this.chatList();
        (document.getElementById("message") as HTMLTextAreaElement).value = '';
      }, error => {
        this.errorHandling.handle(error.status);
      });
  }

  private getMyPosition(): void {
    for (let index = 0; index < this.userPointClassifications.length; index++) {

      var userLogged = this.userLocalstorage.getUserLogged();
      if (this.userPointClassifications[index].userId == userLogged.id) {
        userLogged.position = this.userPointClassifications[index].position;
        this.userLocalstorage.setUserLogged(userLogged);
        this.sharedService.userLogged.emit(userLogged);
        return;
      }
    }
  }
}