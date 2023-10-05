import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-ads-create',
  templateUrl: './my-ads-create.component.html',
  styleUrls: ['./my-ads-create.component.css']
})
export class MyAdsCreateComponent {
  validated: boolean = false;

  constructor(private router: Router, private formBuilder: FormBuilder) { }

  createAdForm = this.formBuilder.group({
    title: ['', Validators.required],
    description: ['', Validators.required]
  });

  onClickBackToList(): void {
    this.backToList();
  }

  onSubmitSave(): void {
    this.validated = true;

    if (this.createAdForm.invalid)
      return;

    alert('save it!');
  }

  private backToList(): void {
    this.router.navigate(['app/my-ads']);
  }
}
