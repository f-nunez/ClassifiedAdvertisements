import { DataTablePaginatorSetting } from './data-table-paginator/data-table-paginator-setting';

export class DataTableSetting {
    columns: DataTableColumn[] = [];
    defaultSortColumnPropertyName: string = '';
    defaultSortColumnIsAscending: boolean = false;
    paginatorSetting = new DataTablePaginatorSetting();
    tableClass: string = 'table table-hover align-middle';
    headerColumnClassAscending: string = 'bi bi-arrow-up';
    headerColumnClassDescending: string = 'bi bi-arrow-down';
}

export class DataTableColumn {
    bodyClass: string = '';
    propertyName: string = '';
    headerClass: string = '';
    sortable: boolean = false;
    text: string = '';
}

export class DataTableLoadDataEvent {
    skip: number = 0;
    take: number = 10;
    sortColumnPropertyName: string = '';
    sortIsAscending: boolean = false;
}

export class DataTableRowTemplateContext<T = unknown> {
    public $implicit: T = null!;
}
