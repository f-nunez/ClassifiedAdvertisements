import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { DataTablePaginatorSelectPageEvent } from './data-table-paginator-select-page-event';
import { DataTablePaginatorPageNumber } from './data-table-paginator-page-number';
import { DataTablePaginatorSetting } from './data-table-paginator-setting';

@Component({
  selector: 'app-data-table-paginator',
  templateUrl: './data-table-paginator.component.html',
  styleUrls: ['./data-table-paginator.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DataTablePaginatorComponent implements OnInit, OnChanges {
  @Input() data: any[] = [];
  @Input() paginatorSetting = new DataTablePaginatorSetting();
  @Input() totalRecordsCount: number = 0;
  @Output() selectPage = new EventEmitter<DataTablePaginatorSelectPageEvent>();
  isFirstEnabled: boolean = false;
  isPreviousEnabled: boolean = false;
  isNextEnabled: boolean = false;
  isLastEnabled: boolean = false;
  maxPageNumbersToShow: number = 7;
  newSelectedPageNumber: number = 1;
  pageNumbers: DataTablePaginatorPageNumber[] = [];
  selectedPageNumber: number = 1;
  selectedPageSize: number = 10;
  totalPageNumbers: number = 0;

  ngOnChanges(changes: SimpleChanges): void {
    this.selectedPageNumber = this.newSelectedPageNumber;
    this.processPagination();
  }

  ngOnInit(): void {
    this.selectedPageSize = this.paginatorSetting.defaultPageSize;
  }

  onClickPageNumber(pageNumber: number): void {
    this.sendSelectedPageEvent(pageNumber);
  }

  onClickFirst(): void {
    this.sendSelectedPageEvent(1);
  }

  onClickPrevious(): void {
    this.sendSelectedPageEvent(this.selectedPageNumber - 1);
  }

  onClickLast(): void {
    this.sendSelectedPageEvent(this.totalPageNumbers);
  }

  onClickNext(): void {
    this.sendSelectedPageEvent(this.selectedPageNumber + 1);
  }

  onSelectPageSize(take: number): void {
    this.selectedPageSize = take;
    this.sendSelectedPageEvent(1);
  }

  private sendSelectedPageEvent(pageNumber: number): void {
    this.newSelectedPageNumber = pageNumber;

    let triggerPagination: DataTablePaginatorSelectPageEvent = {
      pageNumber: pageNumber,
      pageSize: this.selectedPageSize
    };

    this.selectPage.emit(triggerPagination);
  }

  private processPagination(): void {
    this.totalPageNumbers = this.getTotalPageNumbers(
      this.totalRecordsCount,
      this.selectedPageSize
    );

    this.validateSelectedPageNumber();

    let startFrom = 1;
    let endTo = this.totalPageNumbers;

    if (this.totalPageNumbers > this.maxPageNumbersToShow) {
      let leftRemainsToAddAtRightSide = 0;
      let rightRemainsToAddAtLeftSide = 0;

      let leftSide = this.selectedPageNumber - 3;

      if (leftSide < 1) {
        leftRemainsToAddAtRightSide = Math.abs(leftSide) + 1;
        leftSide = 1;
      }

      let rightSide = this.selectedPageNumber + 3;

      if (rightSide > this.totalPageNumbers) {
        rightRemainsToAddAtLeftSide = rightSide - this.totalPageNumbers;
        rightSide = this.totalPageNumbers;
      }

      startFrom = leftSide - rightRemainsToAddAtLeftSide;
      endTo = rightSide + leftRemainsToAddAtRightSide;
    }

    this.pageNumbers = this.mapPageNumbers(
      startFrom,
      endTo,
      this.selectedPageNumber
    );

    this.updateStepActions();
  }

  private getTotalPageNumbers(
    totalRecordsCount: number,
    pageSize: number
  ): number {
    return totalRecordsCount < pageSize ? 1 : Math.ceil(totalRecordsCount / pageSize);
  };

  private mapPageNumbers(
    startFrom: number,
    endTo: number,
    currentPageNumber: number
  ): DataTablePaginatorPageNumber[] {
    let pageNumberss: DataTablePaginatorPageNumber[] = [];

    for (let i = startFrom; i <= endTo; i++)
      pageNumberss.push(this.mapPageNumber(i, currentPageNumber));

    return pageNumberss;
  }

  private mapPageNumber(
    pageNumber: number,
    currentPageNumber: number
  ): DataTablePaginatorPageNumber {
    return {
      isSelected: currentPageNumber === pageNumber,
      number: pageNumber,
      text: `${pageNumber}`
    }
  }

  private updateStepActions(): void {
    this.isFirstEnabled = this.selectedPageNumber > 1;
    this.isPreviousEnabled = this.isFirstEnabled;
    this.isLastEnabled = this.selectedPageNumber < this.totalPageNumbers;
    this.isNextEnabled = this.isLastEnabled;
  }

  private validateSelectedPageNumber() {
    if (this.selectedPageNumber < 1)
      this.selectedPageNumber = 1;

    if (this.selectedPageNumber > this.totalPageNumbers)
      this.selectedPageNumber = this.totalPageNumbers;
  }
}
