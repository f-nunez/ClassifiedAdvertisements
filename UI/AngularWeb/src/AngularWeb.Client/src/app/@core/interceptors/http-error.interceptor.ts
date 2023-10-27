import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SpinnerOverlayService } from '@core/services/spinner-overlay.service';
import { Observable, retry, finalize, delay } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class HttpErrorInterceptor implements HttpInterceptor {
    private readonly retryCount = 3;
    private readonly retryDelayInMilliseconds = 1000;

    constructor(private spinnerOverlayService: SpinnerOverlayService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.spinnerOverlayService.implicitShow();

        return next.handle(req).pipe(
            delay(500),// TODO: remove fake delay after implement state subscriptions
            retry({ count: this.retryCount, delay: this.retryDelayInMilliseconds }),
            finalize(() => {
                this.spinnerOverlayService.implicitHide();
            })
        );
    }
}
