import { HttpInterceptor, HttpHandler, HttpEvent, HttpRequest, HttpHeaders } from "@angular/common/http";
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import { SharedService } from "../services/shared.service";
import { UserLocalstorage } from "../localstorage/user.localstorage";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    shared: SharedService;

    constructor(private userLocalstorage: UserLocalstorage) {
        this.shared = SharedService.getInstance();
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        var userLoggedLocalStorage = this.userLocalstorage.getUserLogged();
        if (userLoggedLocalStorage != null) {
            this.shared.user = userLoggedLocalStorage;
            this.shared.showTemplate.emit(true)

            const clonedRequest = request.clone({
                headers: request.headers
                    .set('token', this.shared.user.token)
                    .set('userId', this.shared.user.id.toString())
            });

            return next.handle(clonedRequest);
        }

        return next.handle(request);
    }
}