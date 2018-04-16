import { HttpInterceptor, HttpHandler, HttpEvent, HttpRequest, HttpHeaders } from "@angular/common/http";
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import { SharedService } from "../../services/shared.service";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    shared: SharedService;

    constructor() {
        this.shared = SharedService.getInstance();
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        if (this.shared.isLoggedIn()) {
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