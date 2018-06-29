import { AuthGuard } from './security/auth.guard';
import { UserService } from './services/user.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { MenuComponent } from './components/menu/menu.component';
import { FooterComponent } from './components/footer/footer.component';
import { LoginComponent } from './components/login/login.component';
import { routes } from './app.routes';
import { SharedService } from './services/shared.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AuthInterceptor } from './security/auth.interceptor';
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
import { ClassificationComponent } from './components/classification/classification.component';
import { ErrorInterceptor } from './security/error.interceptor';
import { BetComponent } from './components/reports/bet/bet.component';
import { ReportBet } from './models/report-bet.model';
import { ReportService } from './services/report.service';
import { BetByGameComponent } from './components/reports/bet-by-game/bet-by-game.component';
import { OutComponent } from './components/out/out.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    MenuComponent,
    FooterComponent,
    LoginComponent,
    UserNewComponent,
    UserGameComponent,
    OficialGameComponent,
    RuleComponent,
    TruncatePipe,
    ClassificationComponent,
    BetComponent,
    BetByGameComponent,
    OutComponent
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
    ReportService,
    UserLocalstorage,
    ErrorInterceptor,
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
