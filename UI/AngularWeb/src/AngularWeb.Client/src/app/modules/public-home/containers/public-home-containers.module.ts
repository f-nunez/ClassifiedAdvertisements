import { NgModule, Type } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { PublicHomeComponent } from './public-home/public-home.component';
import { PublicHomeComponentsModule } from '../components/public-home-components.module';

const containers: Array<Type<any>> = [PublicHomeComponent];

@NgModule({
  declarations: [...containers],
  imports: [SharedModule, PublicHomeComponentsModule],
  exports: [...containers]
})
export class PublicHomeContainersModule { }
