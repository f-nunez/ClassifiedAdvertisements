import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { MyAdsContainersModule } from './containers/my-ads-containers.module';
import { MyAdsRoutingModule } from './my-ads-routing.module';
import { MyAdsService } from './services/my-ads.service';

@NgModule({
  declarations: [],
  imports: [
    SharedModule,
    MyAdsContainersModule,
    MyAdsRoutingModule
  ],
  providers: [MyAdsService]
})
export class MyAdsModule { }
