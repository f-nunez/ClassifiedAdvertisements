import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppThemeHeaderComponent } from './app-theme-header/app-theme-header.component';
import { AppThemeSidebarComponent } from './app-theme-sidebar/app-theme-sidebar.component';
import { SharedModule } from '@shared/shared.module';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppThemeHeaderComponent,
    AppThemeSidebarComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule
  ],
  exports: [
    AppThemeHeaderComponent,
    AppThemeSidebarComponent
  ]
})
export class ComponentsModule { }
