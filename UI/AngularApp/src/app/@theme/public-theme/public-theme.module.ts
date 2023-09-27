import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PublicThemeComponent } from './public-theme.component';
import { ComponentsModule } from './components/components.module';
import { PublicThemeRoutingModule } from './public-theme-routing.module';

@NgModule({
  declarations: [
    PublicThemeComponent
  ],
  imports: [
    CommonModule,
    ComponentsModule,
    PublicThemeRoutingModule
  ],
  exports: [
    PublicThemeComponent
  ]
})
export class PublicThemeModule { }
