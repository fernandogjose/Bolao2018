import { AuthGuard } from './components/security/auth.guard';
import { UserService } from './services/user.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { MenuComponent } from './components/menu/menu.component';
import { FooterComponent } from './components/footer/footer.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { routes } from './app.routes';
import { SharedService } from './services/shared.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AuthInterceptor } from './components/security/auth.interceptor';
import { UserNewComponent } from './components/user-new/user-new.component';
import { UserGameComponent } from './components/user-game/user-game.component';
import { UserGameService } from './services/user-game.service';
import { OficialGameComponent } from './components/oficial-game/oficial-game.component';
import { OficialGameService } from './services/oficial-game.service';
import { RuleComponent } from './components/rule/rule.component';
import { HomeService } from './services/home.service';
import { ChatService } from './services/chat.service';
import { TruncatePipe } from './filters/truncate-pipe.filter';
import { UserLocalstorage } from './localstorage/user.localstorage';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    MenuComponent,
    FooterComponent,
    HomeComponent,
    LoginComponent,
    UserNewComponent,
    UserGameComponent,
    OficialGameComponent,
    RuleComponent,
    TruncatePipe
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    routes
  ],
  providers: [
    UserService,
    UserGameService,
    OficialGameService,
    SharedService,
    HomeService,
    ChatService,
    UserLocalstorage,
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
