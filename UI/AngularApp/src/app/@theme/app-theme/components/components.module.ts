import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppThemeHeaderComponent } from './app-theme-header/app-theme-header.component';
import { AppThemeSidebarComponent } from './app-theme-sidebar/app-theme-sidebar.component';

@NgModule({
  declarations: [
    AppThemeHeaderComponent,
    AppThemeSidebarComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    AppThemeHeaderComponent,
    AppThemeSidebarComponent
  ]
})
export class ComponentsModule { }
