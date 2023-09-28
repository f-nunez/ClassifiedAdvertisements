import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PublicThemeComponent } from './public-theme.component';
import { ComponentsModule } from './components/components.module';
import { PublicThemeRoutingModule } from './public-theme-routing.module';
import { ThemeComponentsModule } from '@theme/components/theme-components.module';

@NgModule({
  declarations: [
    PublicThemeComponent
  ],
  imports: [
    CommonModule,
    ComponentsModule,
    PublicThemeRoutingModule,
    ThemeComponentsModule
  ],
  exports: [
    PublicThemeComponent
  ]
})
export class PublicThemeModule { }
