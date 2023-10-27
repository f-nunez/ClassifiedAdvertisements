import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PublicThemeComponent } from './public-theme.component';
import { PublicThemeComponentsModule } from './components/public-theme-components.module';
import { PublicThemeRoutingModule } from './public-theme-routing.module';
import { ThemeComponentsModule } from '@theme/components/theme-components.module';

@NgModule({
  declarations: [
    PublicThemeComponent
  ],
  imports: [
    PublicThemeComponentsModule,
    PublicThemeRoutingModule,
    ThemeComponentsModule
  ],
  exports: [
    PublicThemeComponent
  ]
})
export class PublicThemeModule { }
