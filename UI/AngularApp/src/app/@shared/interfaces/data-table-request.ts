import { DataTableRequestSort } from './data-table-request-sort';

export interface DataTableRequest {
    skip: number,
    sorts?: DataTableRequestSort[],
    take: number
}
