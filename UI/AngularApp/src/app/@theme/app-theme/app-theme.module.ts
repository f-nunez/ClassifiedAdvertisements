import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppThemeComponent } from './app-theme.component';
import { AppThemeRoutingModule } from './app-theme-routing.module';
import { ComponentsModule } from './components/components.module';
import { SharedModule } from '@shared/shared.module';

@NgModule({
  declarations: [
    AppThemeComponent
  ],
  imports: [
    CommonModule,
    ComponentsModule,
    AppThemeRoutingModule,
    SharedModule
  ],
  exports: [
    AppThemeComponent
  ]
})
export class AppThemeModule { }
