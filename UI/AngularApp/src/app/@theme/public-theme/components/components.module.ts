import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PublicThemeHeaderComponent } from './public-theme-header/public-theme-header.component';

@NgModule({
  declarations: [
    PublicThemeHeaderComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    PublicThemeHeaderComponent
  ]
})
export class ComponentsModule { }
