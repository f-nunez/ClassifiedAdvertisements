import { NgModule } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { UsersContainersModule } from './containers/users-containers.module';
import { UsersRoutingModule } from './users-routing.module';

@NgModule({
  declarations: [],
  imports: [
    SharedModule,
    UsersContainersModule,
    UsersRoutingModule
  ]
})
export class UsersModule { }
