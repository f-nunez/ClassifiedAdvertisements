import { AfterContentInit, Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@core/services/auth.service';
import { ScriptService } from '@core/services/script.service';

@Component({
  selector: 'app-app-theme',
  templateUrl: './app-theme.component.html',
  styleUrls: ['./app-theme.component.css']
})
export class AppThemeComponent implements AfterContentInit {

  constructor(private authService: AuthService, private router: Router, private scriptService: ScriptService) { }

  ngAfterContentInit(): void {
    this.scriptService.loadScripts('theme.js');
  }

  async onSignoutAsync() {
    await this.authService.signoutAsync();

    this.router.navigate(['']);
  }
}
