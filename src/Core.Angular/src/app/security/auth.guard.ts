import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Observable } from 'rxjs/Observable';
import { SharedService } from '../services/shared.service';
import { UserLocalstorage } from '../localstorage/user.localstorage';

@Injectable()
export class AuthGuard implements CanActivate {

    public shared: SharedService;

    constructor(private router: Router, private userLocalstorage: UserLocalstorage) {
        this.shared = SharedService.getInstance();
    }

    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean | Observable<boolean> {

        var userLoggedLocalStorage = this.userLocalstorage.getUserLogged();
        if (userLoggedLocalStorage != null) {
            console.log(userLoggedLocalStorage);
            this.shared.user = userLoggedLocalStorage;
            this.shared.showTemplate.emit(true)
            return true;
        }

        if (this.shared.isLoggedIn()) {
            return true;
        }

        this.router.navigate(['/login']);
        return false;
    }
}