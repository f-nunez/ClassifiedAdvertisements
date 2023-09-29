import { NgModule, Type } from '@angular/core';
import { SharedModule } from '@shared/shared.module';
import { HomeComponent } from './home/home.component';
import { HomeComponentsModule } from '../components/home-components.module';

const containers: Array<Type<any>> = [HomeComponent];

@NgModule({
  declarations: [...containers],
  imports: [SharedModule, HomeComponentsModule],
  exports: [...containers]
})
export class HomeContainersModule { }
