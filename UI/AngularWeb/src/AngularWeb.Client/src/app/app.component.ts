import { Component } from '@angular/core';
import { AuthService } from '@core/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AngularApp';

  constructor(private authService: AuthService) {
    // authService added just to load it starting the AppComponent
  }
}
