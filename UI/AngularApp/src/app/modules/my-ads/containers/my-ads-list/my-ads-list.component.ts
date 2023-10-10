import { Component } from '@angular/core';
import { GetMyAdsListItem } from '../../interfaces/my-ads-list/get-my-ads-list-item';
import { MyAdsService } from '../../services/my-ads.service';
import { GetMyAdsListRequest } from '../../interfaces/my-ads-list/get-my-ads-list-request';
import { Router } from '@angular/router';
import { DataTableSetting } from '@shared/modules/data-table';

@Component({
  selector: 'app-my-ads-list',
  templateUrl: './my-ads-list.component.html',
  styleUrls: ['./my-ads-list.component.css']
})
export class MyAdsListComponent {
  count: number = 0;
  items: GetMyAdsListItem[] = [];
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
    this.getMyAdsList();
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

  private getMyAdsList(): void {
    let request: GetMyAdsListRequest = {
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
