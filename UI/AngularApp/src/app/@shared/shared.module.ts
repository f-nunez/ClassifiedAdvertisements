import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedDirectivesModule } from './directives/shared-directives.module';
import { SharedComponentsModule } from './components/shared-components.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FormsModule,
    SharedComponentsModule,
    SharedDirectivesModule
  ],
  exports: [
    CommonModule,
    FormsModule,
    SharedComponentsModule,
    SharedDirectivesModule
  ],
  providers: []
})
export class SharedModule { }
