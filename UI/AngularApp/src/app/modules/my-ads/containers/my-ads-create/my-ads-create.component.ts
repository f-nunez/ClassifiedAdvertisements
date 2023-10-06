import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MyAdsService } from '../../services/my-ads.service';
import { CreateMyAdRequest } from '../../interfaces/create-my-ad/create-my-ad-request';

@Component({
  selector: 'app-my-ads-create',
  templateUrl: './my-ads-create.component.html',
  styleUrls: ['./my-ads-create.component.css']
})
export class MyAdsCreateComponent {
  form: FormGroup;
  formChanges: number = 0;

  constructor(
    private formBuilder: FormBuilder,
    private myAdsService: MyAdsService,
    private router: Router
  ) {
    this.form = this.formBuilder.group({
      title: ['', Validators.required],
      description: ['', Validators.required]
    });
  }

  onClickBackToList(): void {
    this.backToList();
  }

  onClickCancel(): void {
    this.resetFormGroup();
  }

  onSubmitSave(): void {
    this.form.markAllAsTouched();
    this.formChanges++;

    if (this.form.invalid)
      return;

    let request: CreateMyAdRequest = {
      description: this.form.get('description')?.value ?? '',
      title: this.form.get('title')?.value ?? ''
    };

    this.myAdsService.createMyAd(request).subscribe({
      next: (response) => {
        this.backToList();
      },
      error: (error) => { console.log(error); }
    });
  }

  private backToList(): void {
    this.router.navigate(['app/my-ads']);
  }

  private resetFormGroup(): void {
    this.form.reset();
    this.formChanges++;
  }
}
