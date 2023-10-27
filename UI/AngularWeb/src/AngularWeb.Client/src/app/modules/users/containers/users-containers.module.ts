import { NgModule, Type } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { UsersComponent } from './users/users.component';
import { UsersComponentsModule } from '../components/users-components.module';

const containers: Array<Type<any>> = [UsersComponent];

@NgModule({
  declarations: [...containers],
  imports: [SharedModule, UsersComponentsModule],
  exports: [...containers]
})
export class UsersContainersModule { }
