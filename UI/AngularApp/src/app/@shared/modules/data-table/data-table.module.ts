import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DataTablePaginatorComponent } from './data-table-paginator/data-table-paginator.component';
import { DataTableRowDirective } from './data-table-row.directive';
import { DataTableComponent } from './data-table.component';

@NgModule({
  declarations: [
    DataTableComponent,
    DataTablePaginatorComponent,
    DataTableRowDirective
  ],
  imports: [
    CommonModule, FormsModule
  ],
  exports: [
    DataTableComponent,
    DataTablePaginatorComponent,
    DataTableRowDirective
  ]
})
export class DataTableModule { }
