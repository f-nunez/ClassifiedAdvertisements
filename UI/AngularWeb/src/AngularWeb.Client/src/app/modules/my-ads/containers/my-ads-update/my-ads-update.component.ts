import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MyAdsService } from '../../services/my-ads.service';
import { GetMyAdUpdateRequest } from '../../interfaces/get-my-ad-update/get-my-ad-update-request';
import { GetMyAdUpdate } from '../../interfaces/get-my-ad-update/get-my-ad-update';
import { UpdateMyAdRequest } from '../../interfaces/update-my-ad/update-my-ad-request';

@Component({
  selector: 'app-my-ads-update',
  templateUrl: './my-ads-update.component.html',
  styleUrls: ['./my-ads-update.component.css']
})
export class MyAdsUpdateComponent implements OnInit {
  getMyAdUpdate?: GetMyAdUpdate;
  form: FormGroup;
  formChanges: number = 0;
  validated: boolean = false;

  constructor(
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private myAdsService: MyAdsService,
    private router: Router
  ) {
    this.form = this.formBuilder.group({
      title: ['', Validators.required],
      description: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadData();
  }

  onClickBackToList(): void {
    this.backToList();
  }

  onClickCancel(): void {
    this.restoreFormGroup();
  }

  onSubmitSave(): void {
    this.form.markAllAsTouched();
    this.formChanges++;

    if (this.form.invalid)
      return;

    let request: UpdateMyAdRequest = {
      description: this.form.get('description')?.value ?? '',
      id: this.getMyAdUpdate?.id!,
      title: this.form.get('title')?.value ?? '',
      version: this.getMyAdUpdate?.version!
    };

    this.myAdsService.updateMyAd(request).subscribe({
      next: (response) => {
        this.backToList();
      },
      error: (error) => { console.log(error); }
    });
  }

  private backToList(): void {
    this.router.navigate(['app/my-ads']);
  }

  private loadData(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    if (id == null)
      return;

    let request: GetMyAdUpdateRequest = {
      id: id
    };

    this.myAdsService.getMyAdUpdate(request).subscribe({
      next: (response) => {
        this.getMyAdUpdate = response.getMyAdUpdate;
        this.form.patchValue(this.getMyAdUpdate);
        this.formChanges++;
      },
      error: (error) => { console.log(error); }
    });
  }

  private restoreFormGroup(): void {
    this.form.reset();
    this.form.patchValue(this.getMyAdUpdate!);
    this.formChanges++;
  }
}
