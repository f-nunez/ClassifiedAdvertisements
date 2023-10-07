import { Component } from '@angular/core';
import { GetMyAdsListItem } from '../../interfaces/my-ads-list/get-my-ads-list-item';
import { MyAdsService } from '../../services/my-ads.service';
import { GetMyAdsListRequest } from '../../interfaces/my-ads-list/get-my-ads-list-request';
import { TablePaginatorEvent } from '@shared/components/table-paginator/table-paginator-event';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-ads-list',
  templateUrl: './my-ads-list.component.html',
  styleUrls: ['./my-ads-list.component.css']
})
export class MyAdsListComponent {
  changes: number = 0;
  count: number = 0;
  items: GetMyAdsListItem[] = [];
  skip: number = 0;
  take: number = 10;

  constructor(private myAdService: MyAdsService, private router: Router) {
    this.getMyAdsList(this.skip, this.take);
  }

  onClickAdd(): void {
    this.router.navigate(['app/my-ads/create']);
  }

  onClickDetail(id: string): void {
    this.router.navigate(['app/my-ads/detail', id]);
  }

  onClickEdit(id: string): void {
    this.router.navigate(['app/my-ads/update', id]);
  }

  onPaginationEvent(trigger: TablePaginatorEvent): void {
    this.getMyAdsList(trigger.skip, trigger.take);
    this.changes++;
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
