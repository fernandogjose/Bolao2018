import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Observable } from 'rxjs/Observable';
import { UserLocalstorage } from '../localstorage/user.localstorage';
import { Location } from '@angular/common';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router, private userLocalstorage: UserLocalstorage, private location: Location) { }

    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean | Observable<boolean> {

        var userLoggedLocalStorage = this.userLocalstorage.getUserLogged();
        if (userLoggedLocalStorage != null) {
            return true;
        }

        this.router.navigate(['/login']);
        return false;
    }
}