import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { HomeContainersModule } from './containers/home-containers.module';
import { HomeRoutingModule } from './home-routing.module';

@NgModule({
  declarations: [],
  imports: [
    SharedModule,
    HomeContainersModule,
    HomeRoutingModule
  ]
})
export class HomeModule { }
