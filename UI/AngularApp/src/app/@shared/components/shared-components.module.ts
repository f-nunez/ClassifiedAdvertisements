import { NgModule, Type } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TablePaginatorComponent } from './table-paginator/table-paginator.component';
import { FormInputComponent } from './form-input/form-input.component';
import { FormLabelComponent } from './form-label/form-label.component';
import { FormTextareaComponent } from './form-textarea/form-textarea.component';

export const components: Type<any>[] = [
  FormInputComponent,
  FormLabelComponent,
  FormTextareaComponent,
  TablePaginatorComponent
];

@NgModule({
  declarations: [...components],
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  exports: [...components]
})
export class SharedComponentsModule { }
