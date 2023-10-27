import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GetMyAdDetailRequest } from '../../interfaces/get-my-ad-detail/get-my-ad-detail-request';
import { GetMyAdDetail } from '../../interfaces/get-my-ad-detail/get-my-ad-detail';
import { MyAdsService } from '../../services/my-ads.service';

@Component({
  selector: 'app-my-ads-detail',
  templateUrl: './my-ads-detail.component.html',
  styleUrls: ['./my-ads-detail.component.css']
})
export class MyAdsDetailComponent implements OnInit {
  getMyAdDetail?: GetMyAdDetail;
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
    this.lodData();
  }

  onClickBackToList(): void {
    this.backToList();
  }

  private backToList(): void {
    this.router.navigate(['app/my-ads']);
  }

  private lodData(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    if (id == null)
      return;

    let request: GetMyAdDetailRequest = {
      id: id
    };

    this.myAdsService.getMyAdDetail(request).subscribe({
      next: (response) => {
        this.getMyAdDetail = response.getMyAdDetail;
        this.form.patchValue(this.getMyAdDetail);
        this.formChanges++;
      },
      error: (error) => { console.log(error); }
    });
  }
}
