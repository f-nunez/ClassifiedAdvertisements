import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class SpinnerOverlayService {
    private isExplicitShow: boolean = false;
    private showSubject$ = new BehaviorSubject<any>({ message: '', showSpinner: false });

    public explicitHide(): void {
        this.isExplicitShow = false;
        this.setShowSubject(false);
    }

    public explicitShow(message: string = ''): void {
        this.isExplicitShow = true;
        this.setShowSubject(true, message);
    }

    public implicitHide(): void {
        if (this.isExplicitShow)
            return;

        this.setShowSubject(false);
    }

    public implicitShow(): void {
        if (this.isExplicitShow)
            return;

        this.setShowSubject(true);
    }

    public getShowObservable(): Observable<any> {
        return this.showSubject$.asObservable();
    }

    private setShowSubject(showSpinner: boolean, message: string = ''): void {
        this.showSubject$.next({ message: message, showSpinner: showSpinner });
    }
}
