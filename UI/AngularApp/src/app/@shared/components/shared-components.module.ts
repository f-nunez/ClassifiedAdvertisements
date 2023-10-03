import { NgModule, Type } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TablePaginatorComponent } from './table-paginator/table-paginator.component';
import { FormsModule } from '@angular/forms';

export const components: Type<any>[] = [
  TablePaginatorComponent
];

@NgModule({
  declarations: [...components],
  imports: [CommonModule, FormsModule],
  exports: [...components]
})
export class SharedComponentsModule { }
