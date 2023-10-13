import { DataTableResponse } from '@shared/interfaces/data-table-response';
import { GetMyAdListItem } from './get-my-ad-list-item';

export interface GetMyAdListResponse {
    dataTableResponse: DataTableResponse<GetMyAdListItem>
}
