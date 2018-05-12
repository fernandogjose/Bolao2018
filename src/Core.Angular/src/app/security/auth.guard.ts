import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { SharedService } from '../services/shared.service';
import { UserLocalstorage } from '../localstorage/user.localstorage';
import { Location } from '@angular/common';
import { Observable } from 'rxjs';

@Injectable()
export class AuthGuard implements CanActivate {

    public shared: SharedService;

    constructor(private router: Router, private userLocalstorage: UserLocalstorage, private location: Location) {
        this.shared = SharedService.getInstance();
    }

    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean | Observable<boolean> {

        if (this.shared.isLoggedIn()) {
            return true;
        }

        var userLoggedLocalStorage = this.userLocalstorage.getUserLogged();
        if (userLoggedLocalStorage != null) {
            return true;
        }

        this.router.navigate(['/login']);
        return false;
    }
}