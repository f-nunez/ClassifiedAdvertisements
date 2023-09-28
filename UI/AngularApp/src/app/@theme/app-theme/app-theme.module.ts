import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppThemeComponent } from './app-theme.component';
import { AppThemeRoutingModule } from './app-theme-routing.module';
import { ComponentsModule } from './components/components.module';
import { SharedModule } from '@shared/shared.module';
import { ThemeComponentsModule } from '@theme/components/theme-components.module';

@NgModule({
  declarations: [
    AppThemeComponent
  ],
  imports: [
    CommonModule,
    ComponentsModule,
    AppThemeRoutingModule,
    ThemeComponentsModule,
    SharedModule
  ],
  exports: [
    AppThemeComponent
  ]
})
export class AppThemeModule { }
