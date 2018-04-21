import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL_API } from './config.service';
import { Chat } from '../models/chat.model';
import { ChatCreateRequest } from '../models/chat-create-request.model';

@Injectable()
export class ChatService {

  constructor(private http: HttpClient) { }

  list() {
    return this.http.get(`${BASE_URL_API}/chat`);
  }

  create(chatCreateRequest: ChatCreateRequest) {
    return this.http.post(`${BASE_URL_API}/chat`, chatCreateRequest);
  }

}
