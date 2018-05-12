import { Injectable } from "@angular/core";
import { SharedService } from "../services/shared.service";
import { UserLocalstorage } from "../localstorage/user.localstorage";
import { Router } from "@angular/router";


@Injectable()
export class ErrorInterceptor {
    shared: SharedService;

    constructor(private userLocalstorage: UserLocalstorage, private router: Router) {
        this.shared = SharedService.getInstance();
    }

    get(errorStatus: number) {
        if (errorStatus == 401) {
            this.shared.showTemplate.emit(false);
            this.shared.user = null;
            this.router.navigate(['/login']);
        }
    }
}