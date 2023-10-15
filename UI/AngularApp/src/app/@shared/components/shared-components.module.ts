import { NgModule, Type } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormInputComponent } from './form-input/form-input.component';
import { FormLabelComponent } from './form-label/form-label.component';
import { FormTextareaComponent } from './form-textarea/form-textarea.component';
import { SpinnerOverlayComponent } from './spinner-overlay/spinner-overlay.component';

export const components: Type<any>[] = [
  FormInputComponent,
  FormLabelComponent,
  FormTextareaComponent,
  SpinnerOverlayComponent
];

@NgModule({
  declarations: [...components],
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  exports: [...components]
})
export class SharedComponentsModule { }
