import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-app-theme-header',
  templateUrl: './app-theme-header.component.html',
  styleUrls: ['./app-theme-header.component.css']
})
export class AppThemeHeaderComponent {
  @Output() signout = new EventEmitter<void>();

  onSignout() {
    this.signout.emit();
  }
}
