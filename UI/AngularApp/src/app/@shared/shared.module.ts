import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedDirectivesModule } from './directives/shared-directives.module';
import { SharedComponentsModule } from './components/shared-components.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedComponentsModule,
    SharedDirectivesModule
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedComponentsModule,
    SharedDirectivesModule
  ],
  providers: []
})
export class SharedModule { }
