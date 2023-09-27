import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PublicThemeHeaderComponent } from './public-theme-header/public-theme-header.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    PublicThemeHeaderComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    PublicThemeHeaderComponent
  ]
})
export class ComponentsModule { }
