import { LoginComponent } from './components/login/login.component';
import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import { AuthGuard } from './security/auth.guard';
import { UserNewComponent } from './components/user-new/user-new.component';
import { UserGameComponent } from './components/user-game/user-game.component';
import { OficialGameComponent } from './components/oficial-game/oficial-game.component';
import { RuleComponent } from './components/rule/rule.component';
import { ClassificationComponent } from './components/classification/classification.component';

export const ROUTES: Routes = [
    { path: '', component: LoginComponent },
    { path: 'login', component: LoginComponent },
    { path: 'regulamento', component: RuleComponent },
    { path: 'novo-usuario', component: UserNewComponent },
    { path: 'classificacao', component: ClassificationComponent, canActivate: [AuthGuard] },
    { path: 'meu-jogo', component: UserGameComponent, canActivate: [AuthGuard] },
    { path: 'espiar-jogo/:userId', component: UserGameComponent, canActivate: [AuthGuard] },
    { path: 'resultado-oficial', component: OficialGameComponent, canActivate: [AuthGuard] },
]

export const routes: ModuleWithProviders = RouterModule.forRoot(ROUTES);