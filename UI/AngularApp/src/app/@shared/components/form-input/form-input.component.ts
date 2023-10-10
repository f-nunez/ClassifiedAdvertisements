import { Component, ChangeDetectionStrategy, OnInit, Input, DestroyRef, Self } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ControlValueAccessor, NgControl, FormControl } from '@angular/forms';
import { debounceTime, tap } from 'rxjs';

@Component({
  selector: 'app-form-input',
  templateUrl: './form-input.component.html',
  styleUrls: ['./form-input.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormInputComponent implements ControlValueAccessor, OnInit {
  /**
   * @description
   * Primitive input value for ChangeDetectionStrategy.OnPush
   * @usageNotes
   * At component class (ex: `counter: number = 0`)
   * 
   * At component template (ex: `[changes] = counter`)
   * 
   * Trigger change detector increasing the counter (ex: `this.counter++`)
   */
  @Input() changes?: number;
  @Input() customErrorClass?: string;
  @Input() customInputClass?: string;
  @Input() id?: string;
  @Input() markAsTouchedOnFocus?: boolean
  @Input() placeholder?: string;
  @Input() readonly?: boolean;
  @Input() type?: string;
  /**
   * @description
   * Contains messages for validator errors
   * 
   * @usageNotes
   * Set a list of ValidatorErrorMessage[]
   * 
   * (ex: `validatorErrorMessages = [{ type: 'required', message: 'Required' }]`)
   */
  @Input() validatorErrorMessages: ValidatorErrorMessage[] = [];
  disabled: boolean = false;
  value: string = '';

  constructor(private destroyRef: DestroyRef, @Self() private ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
  }

  writeValue(obj: any): void {
    this.value = obj;
  }

  registerOnChange(fn: any): void {
    this.onChanged = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  ngOnInit(): void {
    this.formControl.valueChanges
      .pipe(
        debounceTime(200),
        tap(value => this.onChanged(value)),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe();
  }

  onChanged = (value: string) => { };

  onFocus(): void {
    if (this.markAsTouchedOnFocus)
      this.onTouched();
  }

  onTouched = () => { };

  onValueChanged(e: any): void {
    if (this.disabled)
      return;

    this.value = e.target.value;
    this.onChanged(this.value);
  }

  get formControl(): FormControl {
    return this.ngControl.control as FormControl;
  }

  get getErrorClass(): string {
    const baseClass = 'invalid-feedback';
    return this.customErrorClass ?? baseClass;
  }

  get getId(): string {
    return this.id ?? '';
  }

  get getInputClass(): string {
    const baseClass = 'form-control';
    return this.customInputClass ?? baseClass;
  }

  get getPlaceholder(): string {
    return this.placeholder ?? '';
  }

  get getReadonly(): boolean {
    return this.readonly ?? false;
  }

  get getType(): string {
    const baseType = 'text';
    return this.type ?? baseType;
  }

  get getValidationClass(): string | null {
    if (!this.formControl.touched)
      return null;

    return this.formControl.invalid ? 'is-invalid' : 'is-valid';
  }

  showErrorMessages(): boolean {
    if (!this.formControl.touched)
      return false;

    return this.formControl.invalid;
  }

  getErrorMessages(): string[] {
    let errorMessages: string[] = [];

    if (!this.validatorErrorMessages.length)
      return errorMessages;

    for (let errorValidator of this.validatorErrorMessages)
      if (this.formControl?.errors?.[errorValidator.type])
        errorMessages.push(errorValidator.message);

    return errorMessages;
  }
}

interface ValidatorErrorMessage {
  type: string,
  message: string
}
