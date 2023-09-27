import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-public-theme-header',
  templateUrl: './public-theme-header.component.html',
  styleUrls: ['./public-theme-header.component.css']
})
export class PublicThemeHeaderComponent {
  @Output() signin = new EventEmitter<void>();

  onSignin() {
    this.signin.emit();
  }
}
