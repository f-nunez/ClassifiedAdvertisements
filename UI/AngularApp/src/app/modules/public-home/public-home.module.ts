import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PublicHomeComponent } from './public-home.component';
import { PublicHomeRoutingModule } from './public-home-routing.module';

@NgModule({
  declarations: [
    PublicHomeComponent
  ],
  imports: [
    CommonModule,
    PublicHomeRoutingModule
  ],
  exports: [
    PublicHomeComponent
  ]
})
export class PublicHomeModule { }
