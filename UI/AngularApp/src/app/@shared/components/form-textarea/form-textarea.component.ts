import { ChangeDetectionStrategy, Component, Input, Self } from '@angular/core';
import { NgControl, FormControl, ControlValueAccessor } from '@angular/forms';

@Component({
  selector: 'app-form-textarea',
  templateUrl: './form-textarea.component.html',
  styleUrls: ['./form-textarea.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormTextareaComponent implements ControlValueAccessor {
  @Input() customInputClass: string = '';
  @Input() customErrorClass: string = '';
  @Input() id: string = '';
  @Input() placeholder: string = '';
  @Input() required: boolean = false;
  @Input() readonly: boolean = false;
  @Input() rows: number = 1;
  @Input() validated: boolean = false;
  /**
   * @description
   * Contains messages for validator errors
   * 
   * @usageNotes
   * Set validationErrors has a list of ValidatorError[]
   * 
   * (ex: `validatorErrors = [{ type: 'required', message: 'Required' }]`)
   */
  @Input() validatorErrors: ValidatorError[] = [];

  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
  }

  writeValue(obj: any): void { }

  registerOnChange(fn: any): void { }

  registerOnTouched(fn: any): void { }

  setDisabledState?(isDisabled: boolean): void { }

  get formControl(): FormControl {
    return this.ngControl.control as FormControl;
  }

  get validationClass(): string {
    if (!this.validated && !this.formControl.touched)
      return '';

    if (this.formControl.invalid || this.formControl.errors)
      return 'is-invalid';

    return 'is-valid';
  }

  get inputClass(): string {
    const baseClass = 'form-control';
    return this.customInputClass === '' ? baseClass : this.customInputClass;
  }

  get errorClass(): string {
    const baseClass = 'invalid-feedback';
    return this.customErrorClass === '' ? baseClass : this.customErrorClass;
  }

  showErrorMessages(): boolean {
    if (!this.validated && !this.formControl.touched)
      return false;

    if (this.formControl.invalid || this.formControl.errors)
      return true;

    return false;
  }

  errorMessages(): string[] {
    let errorMessages: string[] = [];

    if (!this.validatorErrors.length)
      return errorMessages;

    for (let errorValidator of this.validatorErrors)
      if (this.formControl?.errors?.[errorValidator.type])
        errorMessages.push(errorValidator.message);

    return errorMessages;
  }
}

interface ValidatorError {
  type: string,
  message: string
}
