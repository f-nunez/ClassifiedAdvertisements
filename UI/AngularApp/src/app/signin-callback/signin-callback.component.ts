import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@core/services/auth.service';

@Component({
  selector: 'app-signin-callback',
  templateUrl: './signin-callback.component.html',
  styleUrls: ['./signin-callback.component.css']
})
export class SigninCallbackComponent implements OnInit {
  constructor(private authService: AuthService, private router: Router) { }

  async ngOnInit() {
    await this.authService.signinCallbackAsync();
    this.router.navigate(['']);
  }
}
