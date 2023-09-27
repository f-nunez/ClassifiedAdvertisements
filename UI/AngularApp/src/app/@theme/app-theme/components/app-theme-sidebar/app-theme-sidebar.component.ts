import { Component } from '@angular/core';
import { Role } from '@core/enums/role';

@Component({
  selector: 'app-app-theme-sidebar',
  templateUrl: './app-theme-sidebar.component.html',
  styleUrls: ['./app-theme-sidebar.component.css']
})
export class AppThemeSidebarComponent {
  role: typeof Role = Role;
}
