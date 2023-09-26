import { AfterContentInit, Component } from '@angular/core';
import { ScriptService } from '@core/services/script.service';

@Component({
  selector: 'app-public-theme',
  templateUrl: './public-theme.component.html',
  styleUrls: ['./public-theme.component.css']
})
export class PublicThemeComponent implements AfterContentInit {

  constructor(private scriptService: ScriptService) {
  }
  ngAfterContentInit(): void {
    this.scriptService.loadScripts('theme.js');
  }
}
