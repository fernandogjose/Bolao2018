import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import { AuthGuard } from './components/security/auth.guard';
import { UserNewComponent } from './components/user-new/user-new.component';
import { UserGameComponent } from './components/user-game/user-game.component';
import { OficialGameComponent } from './components/oficial-game/oficial-game.component';

export const ROUTES: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'novo-usuario', component: UserNewComponent },
    { path: 'meu-jogo', component: UserGameComponent, canActivate: [AuthGuard] },
    { path: 'jogo-usuario/:userId', component: UserGameComponent, canActivate: [AuthGuard] },
    { path: 'resultado-oficial', component: OficialGameComponent, canActivate: [AuthGuard] },
]

export const routes: ModuleWithProviders = RouterModule.forRoot(ROUTES);