import { Type, NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { MyAdsComponentsModule } from '../components/my-ads-components.module';
import { MyAdsListComponent } from './my-ads-list/my-ads-list.component';

const containers: Array<Type<any>> = [MyAdsListComponent];

@NgModule({
  declarations: [...containers],
  imports: [SharedModule, MyAdsComponentsModule],
  exports: [...containers]
})
export class MyAdsContainersModule { }
