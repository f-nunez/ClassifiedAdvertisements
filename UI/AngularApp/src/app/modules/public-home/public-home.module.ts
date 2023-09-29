import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { PublicHomeContainersModule } from './containers/public-home-containers.module';
import { PublicHomeRoutingModule } from './public-home-routing.module';

@NgModule({
  declarations: [],
  imports: [
    SharedModule,
    PublicHomeContainersModule,
    PublicHomeRoutingModule
  ]
})
export class PublicHomeModule { }
