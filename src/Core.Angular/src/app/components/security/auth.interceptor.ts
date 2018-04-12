import { HttpInterceptor, HttpHandler, HttpEvent, HttpRequest } from "@angular/common/http";
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import { SharedService } from "../../services/shared.service";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    shared: SharedService;

    constructor() {
        this.shared = SharedService.getInstance();
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let authRequest: any;

        if (this.shared.isLoggedIn()) {
            authRequest = req.clone({
                setHeaders: {
                    'Content-Type': 'application/json',
                    'Authorization': this.shared.token
                }
            });

            return next.handle(authRequest);
        } else {

            authRequest = req.clone({
                setHeaders: {
                    'Content-Type': 'application/json; charset=UTF-8'
                }
            });

            return next.handle(authRequest);
        }
    }
}