import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@core/services/auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-signin-callback',
  templateUrl: './signin-callback.component.html',
  styleUrls: ['./signin-callback.component.css']
})
export class SigninCallbackComponent implements OnInit, OnDestroy {
  private subscription: Subscription | undefined;

  constructor(private authService: AuthService, private router: Router) {
  }

  async ngOnInit() {
    this.subscription = this.authService.getAuthenticatedObservable()
      .subscribe((res) => {
        if (res)
          this.router.navigate(['']);
      });

    await this.authService.signinCallbackAsync();
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}
