import { Directive, Input, TemplateRef } from '@angular/core';
import { DataTableComponent } from './data-table.component';
import { DataTableRowTemplateContext } from '.';

@Directive({
    selector: '[dataTableRow]'
})
export class DataTableRowDirective<T> {
    @Input() dataTableRowOf: any;

    constructor(
        private dataTableComponent: DataTableComponent<T>,
        templateRef: TemplateRef<DataTableRowTemplateContext<T>>) {
        this.dataTableComponent.rowTemplate = templateRef;
    }
}
