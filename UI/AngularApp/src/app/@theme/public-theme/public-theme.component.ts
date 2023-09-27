import { AfterContentInit, Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@core/services/auth.service';
import { ScriptService } from '@core/services/script.service';

@Component({
  selector: 'app-public-theme',
  templateUrl: './public-theme.component.html',
  styleUrls: ['./public-theme.component.css']
})
export class PublicThemeComponent implements AfterContentInit {
  constructor(private authService: AuthService, private router: Router, private scriptService: ScriptService) { }

  ngAfterContentInit(): void {
    this.scriptService.loadScripts('theme.js');
  }

  async onSigninAsync() {
    await this.authService.signinAsync();
    this.router.navigate(['app']);
  }
}
