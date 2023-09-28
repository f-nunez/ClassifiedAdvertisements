import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@core/services/auth.service';
import { ThemeColorDropdownItem } from '@theme/models/theme-color-dropdown-item';
import { ThemeColorService } from '@theme/services/theme-color.service';
import { ThemeToggleService } from '@theme/services/theme-toggle.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-app-theme',
  templateUrl: './app-theme.component.html',
  styleUrls: ['./app-theme.component.css']
})
export class AppThemeComponent implements OnInit {
  selectedThemeColorDropdownItem$: Observable<ThemeColorDropdownItem>;
  themeColorDropdownItems$: Observable<ThemeColorDropdownItem[]>;

  constructor(
    private authService: AuthService,
    private router: Router,
    private themeColorService: ThemeColorService,
    private themeToggleService: ThemeToggleService
  ) {
    this.selectedThemeColorDropdownItem$ = this.themeColorService.getSelectedDropdownItemObservable();
    this.themeColorDropdownItems$ = this.themeColorService.getDropdownItemsObservable();
  }

  ngOnInit(): void {
    this.themeToggleService.initializeToggle();
  }

  onSelectColor(item: ThemeColorDropdownItem) {
    this.themeColorService.setSelectedColor(item);
  }

  onToggle() {
    this.themeToggleService.toggle();
  }

  async onSignoutAsync() {
    await this.authService.signoutAsync();
    this.router.navigate(['']);
  }
}
