import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyAdsComponent } from './my-ads.component';
import { MyAdsRoutingModule } from './my-ads-routing.module';

@NgModule({
  declarations: [
    MyAdsComponent
  ],
  imports: [
    CommonModule,
    MyAdsRoutingModule
  ],
  exports: [
    MyAdsComponent
  ]
})
export class MyAdsModule { }
