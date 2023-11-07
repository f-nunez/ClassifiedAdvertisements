import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class CsrfHeaderInterceptor implements HttpInterceptor {

    constructor() { }

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        if (!request.headers.has("X-CSRF")) {
            request = request.clone({
                headers: request.headers.set("X-CSRF", "1"),
            });
        }

        return next.handle(request);
    }
}
