import { Injectable } from '@angular/core';
import { SpinnerOverlayService } from './spinner-overlay.service';

@Injectable({
    providedIn: 'root'
})
export class SpinnerService {
    constructor(private spinnerOverlayService: SpinnerOverlayService) { }

    public hide(): void {
        this.spinnerOverlayService.explicitHide();
    }

    public show(message: string = ''): void {
        this.spinnerOverlayService.explicitShow(message);
    }
}
