import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { DataTableColumn, DataTableLoadDataEvent, DataTableRowTemplateContext, DataTableSetting } from '.';
import { DataTablePaginatorSelectPageEvent } from './data-table-paginator/data-table-paginator-select-page-event';

@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DataTableComponent<T> implements OnInit {
  @Input() setting: DataTableSetting = new DataTableSetting();
  @Input() data: T[] = [];
  @Input() totalRecordsCount: number = 0;
  @Output() loadData = new EventEmitter<DataTableLoadDataEvent>();
  headerColumnClassAscending: string = '';
  headerColumnClassDescending: string = '';
  rowTemplate?: TemplateRef<DataTableRowTemplateContext<T>>;
  selectedPageSize: number = 10;
  selectedPageNumber: number = 1;
  selectedSortColumnIsAscending: boolean = false;
  selectedSortColumnPropertyName: string = '';

  constructor() {
  }

  ngOnInit(): void {
    this.headerColumnClassAscending = this.setting.headerColumnClassAscending;
    this.headerColumnClassDescending = this.setting.headerColumnClassDescending;
    this.selectedPageSize = this.setting.paginatorSetting.defaultPageSize;
    this.selectedSortColumnIsAscending = this.setting.defaultSortColumnIsAscending;
    this.selectedSortColumnPropertyName = this.setting.defaultSortColumnPropertyName;
    this.setting.paginatorSetting = this.setting.paginatorSetting;
    this.sendLoadDataEvent(this.selectedPageNumber, this.selectedPageSize);
  }

  onClickHeadColumn(column: DataTableColumn): void {
    if (!column.sortable)
      return;

    if (this.selectedSortColumnPropertyName === column.propertyName) {
      this.selectedSortColumnIsAscending = !this.selectedSortColumnIsAscending;
    }
    else {
      this.selectedSortColumnPropertyName = column.propertyName;
      this.selectedSortColumnIsAscending
    }

    this.sendLoadDataEvent(this.selectedPageNumber, this.selectedPageSize);
  }

  onSelectPage(trigger: DataTablePaginatorSelectPageEvent): void {
    this.selectedPageNumber = trigger.pageNumber;
    this.selectedPageSize = trigger.pageSize;
    this.sendLoadDataEvent(trigger.pageNumber, trigger.pageSize);
  }

  getColumnData(rowData: T, columnPropertyName: string) {
    type ObjectKey = keyof typeof rowData;
    const propertyKey = columnPropertyName as ObjectKey;
    let propertyContent = rowData[propertyKey];
    return !!propertyContent ? propertyContent : '';
  }

  getHeadColumnClass(column: DataTableColumn): string {
    if (column.sortable)
      return `pointer ${column.headerClass}`;
    else
      return column.headerClass;
  }

  showSortAscending(column: DataTableColumn): boolean {
    return this.selectedSortColumnPropertyName === column.propertyName
      && this.selectedSortColumnIsAscending;
  }

  showSortDescending(column: DataTableColumn): boolean {
    return this.selectedSortColumnPropertyName === column.propertyName
      && !this.selectedSortColumnIsAscending;
  }

  private sendLoadDataEvent(pageNumber: number, pageSize: number): void {
    let eventData = new DataTableLoadDataEvent();
    eventData.skip = this.getSkip(pageNumber, pageSize);
    eventData.take = pageSize;

    if (this.selectedSortColumnPropertyName != '') {
      eventData.sortColumnPropertyName = this.selectedSortColumnPropertyName;
      eventData.sortIsAscending = this.selectedSortColumnIsAscending;
    }

    this.loadData.emit(eventData);
  }

  private getSkip(pageNumber: number, pageSize: number): number {
    return (pageNumber - 1) * pageSize;
  }
}
