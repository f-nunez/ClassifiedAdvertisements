import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MyAdsService } from '../../services/my-ads.service';
import { CreateMyAdRequest } from '../../interfaces/create-my-ad/create-my-ad-request';

@Component({
  selector: 'app-my-ads-create',
  templateUrl: './my-ads-create.component.html',
  styleUrls: ['./my-ads-create.component.css']
})
export class MyAdsCreateComponent {
  validated: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private myAdsService: MyAdsService
  ) { }

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

    let createMyAdRequest: CreateMyAdRequest = {
      description: this.createAdForm.get('description')?.value ?? '',
      title: this.createAdForm.get('title')?.value ?? ''
    };

    this.myAdsService.createMyAd(createMyAdRequest).subscribe({
      next: (response) => {
      },
      error: (error) => { console.log(error); }
    });

    this.backToList();
  }

  private backToList(): void {
    this.router.navigate(['app/my-ads']);
  }
}
