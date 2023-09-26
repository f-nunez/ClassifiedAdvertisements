import { AfterContentInit, Component } from '@angular/core';
import { ScriptService } from '@core/services/script.service';

@Component({
  selector: 'app-app-theme',
  templateUrl: './app-theme.component.html',
  styleUrls: ['./app-theme.component.css']
})
export class AppThemeComponent implements AfterContentInit {

  constructor(private scriptService: ScriptService) {
  }
  ngAfterContentInit(): void {
    this.scriptService.loadScripts('theme.js');
  }
}
