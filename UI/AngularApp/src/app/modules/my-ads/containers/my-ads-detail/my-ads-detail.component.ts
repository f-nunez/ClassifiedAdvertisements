import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GetMyAdDetailRequest } from '../../interfaces/get-my-ad-detail/get-my-ad-detail-request';
import { GetMyAdUpdateItem } from '../../interfaces/get-my-ad-update/get-my-ad-update-item';
import { MyAdsService } from '../../services/my-ads.service';

@Component({
  selector: 'app-my-ads-detail',
  templateUrl: './my-ads-detail.component.html',
  styleUrls: ['./my-ads-detail.component.css']
})
export class MyAdsDetailComponent implements OnInit {
  item?: GetMyAdUpdateItem;
  form: FormGroup;
  formChanges: number = 0;

  constructor(
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private myAdsService: MyAdsService,
    private router: Router
  ) {
    this.form = this.formBuilder.group({
      title: [],
      description: []
    });
  }

  ngOnInit(): void {
    this.getMyAdDetail();
  }

  onClickBackToList(): void {
    this.backToList();
  }

  private backToList(): void {
    this.router.navigate(['app/my-ads']);
  }

  private getMyAdDetail(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    if (id == null)
      return;

    let request: GetMyAdDetailRequest = {
      id: id
    };

    this.myAdsService.getMyAdDetail(request).subscribe({
      next: (response) => {
        this.item = response.item;
        this.form.patchValue(this.item);
        this.formChanges++;
      },
      error: (error) => { console.log(error); }
    });
  }
}
