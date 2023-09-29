import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@core/services/auth.service';
import { ThemeColorDropdownItem } from '@theme/interfaces/theme-color-dropdown-item';
import { ThemeColorService } from '@theme/services/theme-color.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-public-theme',
  templateUrl: './public-theme.component.html',
  styleUrls: ['./public-theme.component.css']
})
export class PublicThemeComponent {
  selectedThemeColorDropdownItem$: Observable<ThemeColorDropdownItem>;
  themeColorDropdownItems$: Observable<ThemeColorDropdownItem[]>;

  constructor(
    private authService: AuthService,
    private router: Router,
    private themeColorService: ThemeColorService
  ) {
    this.selectedThemeColorDropdownItem$ = this.themeColorService.getSelectedDropdownItemObservable();
    this.themeColorDropdownItems$ = this.themeColorService.getDropdownItemsObservable();
  }

  onSelectColor(item: ThemeColorDropdownItem) {
    this.themeColorService.setSelectedColor(item);
  }

  async onSigninAsync() {
    await this.authService.signinAsync();
    this.router.navigate(['app']);
  }
}
