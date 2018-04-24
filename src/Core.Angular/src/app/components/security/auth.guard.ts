import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Observable } from 'rxjs/Observable';
import { SharedService } from '../../services/shared.service';

@Injectable()
export class AuthGuard implements CanActivate {

    public shared: SharedService;

    constructor(private router: Router) {
        this.shared = SharedService.getInstance();
    }

    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean | Observable<boolean> {

        if (this.shared.isLoggedIn()) {
            return true;
        }

        var userLoggedLocalStorage = JSON.parse(localStorage.getItem("userLoggedLocalStorage"));
        if (userLoggedLocalStorage != null) {
            this.shared.user = userLoggedLocalStorage;
            this.shared.showTemplate.emit(true)
            return true;
        }

        this.router.navigate(['/login']);
        return false;
    }
}