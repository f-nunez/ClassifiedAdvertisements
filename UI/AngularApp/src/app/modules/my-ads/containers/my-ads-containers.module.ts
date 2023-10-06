import { Type, NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { MyAdsComponentsModule } from '../components/my-ads-components.module';
import { MyAdsCreateComponent } from './my-ads-create/my-ads-create.component';
import { MyAdsListComponent } from './my-ads-list/my-ads-list.component';
import { MyAdsUpdateComponent } from './my-ads-update/my-ads-update.component';

const containers: Array<Type<any>> = [
  MyAdsCreateComponent,
  MyAdsListComponent,
  MyAdsUpdateComponent
];

@NgModule({
  declarations: [...containers],
  imports: [SharedModule, MyAdsComponentsModule],
  exports: [...containers]
})
export class MyAdsContainersModule { }
