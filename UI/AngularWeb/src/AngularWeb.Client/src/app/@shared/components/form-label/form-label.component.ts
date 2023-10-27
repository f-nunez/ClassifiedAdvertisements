import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

@Component({
  selector: 'app-form-label',
  templateUrl: './form-label.component.html',
  styleUrls: ['./form-label.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormLabelComponent {
  @Input() customClass: string = '';
  @Input() for: string = '';
  @Input() text: string = '';

  get labelClass(): string {
    const baseClass = 'form-label fw-semibold';
    return this.customClass === '' ? baseClass : this.customClass;
  }
}
