import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DirectivesModule } from './directives/directives.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    DirectivesModule
  ],
  exports: [
    CommonModule,
    DirectivesModule
  ],
  providers: []
})
export class SharedModule { }
