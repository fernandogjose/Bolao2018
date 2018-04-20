import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from '../../services/shared.service';
import { UserPointClassification } from '../../models/user-point-classification.model';
import { HomeService } from '../../services/home.service';
import { ChatService } from '../../services/chat.service';
import { Chat } from '../../models/chat.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  @ViewChild("form")
  form: NgForm
  
  shared: SharedService;
  userPointClassifications: UserPointClassification[];
  chats: Chat[];
  chat: Chat;
  loading: boolean;

  constructor(private chatService: ChatService, private homeService: HomeService, private router: Router) {
    this.shared = SharedService.getInstance();
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

    this.chatService
      .create(this.chat)
      .subscribe(() => {
        this.loading = false;
        this.form.resetForm();
      }, error => {
        if (error.status == 401) {
          this.shared.showTemplate.emit(false);
          this.shared.user = null;
          this.router.navigate(['/login']);
          this.loading = false;
        }
      });
  }
}