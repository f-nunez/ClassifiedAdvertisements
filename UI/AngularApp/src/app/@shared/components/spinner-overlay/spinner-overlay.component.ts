import { Component, OnDestroy } from '@angular/core';
import { SpinnerOverlayService } from '@core/services/spinner-overlay.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-spinner-overlay',
  templateUrl: './spinner-overlay.component.html',
  styleUrls: ['./spinner-overlay.component.css']
})
export class SpinnerOverlayComponent implements OnDestroy {
  message: string = '';
  showSpinner: boolean = false;
  showSpinnerSubscription = new Subscription();

  constructor(private spinnerOverlayService: SpinnerOverlayService) {
    this.showSpinnerSubscription = this.spinnerOverlayService
      .getShowObservable()
      .subscribe(next => {
        this.showSpinner = next.showSpinner;
        this.message = next.message;
      });
  }

  ngOnDestroy(): void {
    this.showSpinnerSubscription.unsubscribe();
  }

  get show(): boolean {
    return this.showSpinner;
  }
}
