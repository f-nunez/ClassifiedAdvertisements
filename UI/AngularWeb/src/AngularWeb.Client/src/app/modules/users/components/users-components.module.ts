import { NgModule, Type } from '@angular/core';
import { SharedModule } from '@shared/shared.module';

const components: Array<Type<any>> = [];

@NgModule({
  declarations: [...components],
  imports: [SharedModule],
  exports: [...components]
})
export class UsersComponentsModule { }
