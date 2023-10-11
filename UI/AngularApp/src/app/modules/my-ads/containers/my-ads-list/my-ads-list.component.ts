import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DataTableSetting } from '@shared/modules/data-table';
import { DeleteMyAdRequest } from '../../interfaces/delete-my-ad/delete-my-ad-request';
import { GetMyAdListItem } from '../../interfaces/get-my-ad-list/get-my-ad-list-item';
import { GetMyAdListRequest } from '../../interfaces/get-my-ad-list/get-my-ad-list-request';
import { MyAdsService } from '../../services/my-ads.service';

@Component({
  selector: 'app-my-ads-list',
  templateUrl: './my-ads-list.component.html',
  styleUrls: ['./my-ads-list.component.css']
})
export class MyAdsListComponent {
  count: number = 0;
  items: GetMyAdListItem[] = [];
  skip: number = 0;
  take: number = 10;
  sortColumnPropertyName: string = '';
  sortIsAscending: boolean = false;
  dataTableSetting: DataTableSetting;

  constructor(
    private myAdsService: MyAdsService,
    private router: Router
  ) {
    this.dataTableSetting = this.getDataTableSetting();
  }

  onLoadData(value: any) {
    this.skip = value.skip;
    this.take = value.take;
    this.sortColumnPropertyName = value.sortColumnPropertyName;
    this.sortIsAscending = value.sortIsAscending;
    this.loadData();
  }

  onClickAdd(): void {
    this.router.navigate(['app/my-ads/create']);
  }

  onClickDelete(id: string): void {
    this.deleteMyAd(id);
  }

  onClickDetail(id: string): void {
    this.router.navigate(['app/my-ads/detail', id]);
  }

  onClickEdit(id: string): void {
    this.router.navigate(['app/my-ads/update', id]);
  }

  private deleteMyAd(id: string): void {
    let request: DeleteMyAdRequest = { id: id };
    this.myAdsService.deleteMyAd(request).subscribe({
      next: (response) => {
        this.loadData();
      },
      error: (error) => { console.log(error); }
    });
  }

  private getDataTableSetting(): DataTableSetting {
    this.dataTableSetting = new DataTableSetting();
    this.dataTableSetting.defaultSortColumnIsAscending = true;
    this.dataTableSetting.defaultSortColumnPropertyName = 'updatedOn';
    this.dataTableSetting.columns = [
      { bodyClass: '', propertyName: 'title', headerClass: '', sortable: true, text: 'Title' },
      { bodyClass: '', propertyName: 'description', headerClass: '', sortable: true, text: 'Description' },
      { bodyClass: '', propertyName: 'updatedOn', headerClass: '', sortable: true, text: 'Updated On' },
      { bodyClass: '', propertyName: 'publishedOn', headerClass: '', sortable: true, text: 'Published On' },
      { bodyClass: 'actions', propertyName: 'actions', headerClass: '', sortable: false, text: 'Actions' }
    ];

    return this.dataTableSetting;
  }

  private loadData(): void {
    let request: GetMyAdListRequest = {
      skip: this.skip,
      take: this.take,
      sortColumn: this.sortColumnPropertyName,
      sortAscending: this.sortIsAscending
    };

    this.myAdsService.getMyAdsList(request).subscribe({
      next: (response) => {
        this.count = response.count;
        this.items = response.items;
      },
      error: (error) => { console.log(error); }
    });
  }
}
