import { Component } from '@angular/core';
import { GetMyAdsListItem } from '../../interfaces/my-ads-list/get-my-ads-list-item';
import { MyAdsService } from '../../services/my-ads.service';
import { GetMyAdsListRequest } from '../../interfaces/my-ads-list/get-my-ads-list-request';

@Component({
  selector: 'app-my-ads-list',
  templateUrl: './my-ads-list.component.html',
  styleUrls: ['./my-ads-list.component.css']
})
export class MyAdsListComponent {
  count: number = 0;
  items: GetMyAdsListItem[] = [];

  constructor(private myAdService: MyAdsService) { }

  ngOnInit(): void {
    this.getMyAdsList(0, 10);
  }

  private getMyAdsList(skip: number, take: number): void {
    let request: GetMyAdsListRequest = {
      skip: skip,
      take: take
    };

    this.myAdService.getMyAdsList(request).subscribe({
      next: (response) => {
        this.count = response.count;
        this.items = response.items;
      },
      error: (error) => { console.log(error); }
    });
  }
}
