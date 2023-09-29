import { NgModule, Type } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { MyAdsComponent } from './my-ads/my-ads.component';
import { MyAdsComponentsModule } from '../components/my-ads-components.module';

const containers: Array<Type<any>> = [MyAdsComponent];

@NgModule({
  declarations: [...containers],
  imports: [SharedModule, MyAdsComponentsModule],
  exports: [...containers]
})
export class MyAdsContainersModule { }
