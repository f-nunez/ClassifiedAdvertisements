import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@core/services/auth.service';
import { ThemeToggleService } from '@theme/services/theme-toggle.service';

@Component({
  selector: 'app-app-theme',
  templateUrl: './app-theme.component.html',
  styleUrls: ['./app-theme.component.css']
})
export class AppThemeComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private router: Router,
    private themeToggleService: ThemeToggleService
  ) {
  }

  ngOnInit(): void {
    this.themeToggleService.initializeToggle();
  }

  onToggle() {
    this.themeToggleService.toggle();
  }

  async onSignoutAsync() {
    await this.authService.signoutAsync();
    this.router.navigate(['']);
  }
}
