import { HttpInterceptor, HttpHandler, HttpEvent, HttpRequest, HttpHeaders } from "@angular/common/http";
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import { UserLocalstorage } from "../localstorage/user.localstorage";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private userLocalstorage: UserLocalstorage) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        var userLogged = this.userLocalstorage.getUserLogged();
        if (userLogged != null) {
            const clonedRequest = request.clone({
                headers: request.headers
                    .set('token', userLogged.token)
                    .set('userId', userLogged.id.toString())
            });

            return next.handle(clonedRequest);
        }

        return next.handle(request);
    }
}