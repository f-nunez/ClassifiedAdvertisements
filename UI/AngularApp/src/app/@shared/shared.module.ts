import { NgModule, Type } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedDirectivesModule } from './directives/shared-directives.module';
import { SharedComponentsModule } from './components/shared-components.module';
import { DataTableModule } from './modules/data-table/data-table.module';

export const modules: Type<any>[] = [
  DataTableModule
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedComponentsModule,
    SharedDirectivesModule,
    ...modules
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedComponentsModule,
    SharedDirectivesModule,
    ...modules
  ],
  providers: []
})
export class SharedModule { }
