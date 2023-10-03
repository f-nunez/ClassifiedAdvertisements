import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { TablePaginatorPageNumber } from './table-paginator-page-number';
import { TablePaginatorEvent } from './table-paginator-event';

@Component({
  selector: 'app-table-paginator',
  templateUrl: './table-paginator.component.html',
  styleUrls: ['./table-paginator.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TablePaginatorComponent implements OnChanges {
  @Input() changes: number = 0;
  @Input() count: number = 0;
  @Output() paginationEvent = new EventEmitter<TablePaginatorEvent>();
  pageNumbers: TablePaginatorPageNumber[] = [];
  skip: number = 0;
  take: number = 10;
  isFirstEnabled: boolean = false;
  isPreviousEnabled: boolean = false;
  isNextEnabled: boolean = false;
  isLastEnabled: boolean = false;
  maxPageNumbersToShow: number = 7;
  newSelectedPageNumber: number = 1;
  selectedPageNumber: number = 1;
  totalPageNumbers: number = 0;

  ngOnChanges(changes: SimpleChanges): void {
    this.selectedPageNumber = this.newSelectedPageNumber;
    this.processPagination();
  }

  onClickPageNumber(pageNumber: number): void {
    this.firePaginationEvent(pageNumber);
  }

  onClickFirst(): void {
    this.firePaginationEvent(1);
  }

  onClickPrevious(): void {
    this.firePaginationEvent(this.selectedPageNumber - 1);
  }

  onClickLast(): void {
    this.firePaginationEvent(this.totalPageNumbers);
  }

  onClickNext(): void {
    this.firePaginationEvent(this.selectedPageNumber + 1);
  }

  onSelectPageSize(take: number): void {
    this.take = take;
    this.firePaginationEvent(1);
  }

  private firePaginationEvent(pageNumber: number): void {
    this.newSelectedPageNumber = pageNumber;

    this.skip = this.getSkip(this.newSelectedPageNumber, this.take);

    let triggerPagination: TablePaginatorEvent = {
      skip: this.skip,
      take: this.take
    };

    this.paginationEvent.emit(triggerPagination);
  }

  private processPagination(): void {
    this.totalPageNumbers = this.getTotalPageNumbers(this.count, this.take);
    let pageNumbers: TablePaginatorPageNumber[] = [];

    if (this.selectedPageNumber < 1)
      this.selectedPageNumber = 1;

    if (this.selectedPageNumber > this.totalPageNumbers)
      this.selectedPageNumber = this.totalPageNumbers;

    let startFrom = 1;
    let endTo = this.totalPageNumbers;

    if (this.totalPageNumbers > this.maxPageNumbersToShow) {
      let leftSideRemains = 0;
      let rightSideRemains = 0;

      let leftSide = this.selectedPageNumber - 3;

      if (leftSide < 1) {
        leftSideRemains = Math.abs(leftSide) + 1;
        leftSide = 1;
      }

      let rightSide = this.selectedPageNumber + 3;

      if (rightSide > this.totalPageNumbers) {
        rightSideRemains = rightSide - this.totalPageNumbers;
        rightSide = this.totalPageNumbers;
      }

      startFrom = leftSide - rightSideRemains;
      endTo = rightSide + leftSideRemains;
    }

    for (let i = startFrom; i <= endTo; i++) {
      let pageNumber: TablePaginatorPageNumber = {
        isSelected: this.selectedPageNumber === i,
        number: i,
        text: `${i}`
      };

      pageNumbers.push(pageNumber);
    }

    this.pageNumbers = pageNumbers;


    this.isFirstEnabled = this.selectedPageNumber > 1;
    this.isPreviousEnabled = this.isFirstEnabled;
    this.isLastEnabled = this.selectedPageNumber < this.totalPageNumbers;
    this.isNextEnabled = this.isLastEnabled;
  }

  getSelectedPage(skip: number, take: number): number {
    let selectedPageNumber = Math.ceil((skip + 1) / take);
    selectedPageNumber = selectedPageNumber > 0 ? selectedPageNumber : 1;

    return selectedPageNumber;
  }

  getSkip(pageNumber: number, take: number): number {
    let skip = (pageNumber - 1) * take;

    return skip;
  }

  private getTotalPageNumbers(count: number, take: number): number {
    return count < take ? 1 : Math.ceil(count / take);
  };
}
