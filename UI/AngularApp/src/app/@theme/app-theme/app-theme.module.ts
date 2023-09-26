import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppThemeComponent } from './app-theme.component';
import { AppThemeRoutingModule } from './app-theme-routing.module';
import { ComponentsModule } from './components/components.module';

@NgModule({
  declarations: [
    AppThemeComponent
  ],
  imports: [
    CommonModule,
    ComponentsModule,
    AppThemeRoutingModule
  ],
  exports: [
    AppThemeComponent
  ]
})
export class AppThemeModule { }
