import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { GetMyAdsListItem } from '../interfaces/my-ads-list/get-my-ads-list-item';
import { GetMyAdsListRequest } from '../interfaces/my-ads-list/get-my-ads-list-request';
import { GetMyAdsListResponse } from '../interfaces/my-ads-list/get-my-ads-list-response';

@Injectable()
export class MyAdsService {
    public getMyAdsList(request: GetMyAdsListRequest): Observable<GetMyAdsListResponse> {
        let items: GetMyAdsListItem[] = [];

        // TODO: http request to the query api
        // let params = new HttpParams();
        // params = params.append('skip', request.skip);
        // params = params.append('take', request.take);
        // response = httpClient.get<GetMyAdsListResponse>('apiQueryUrl' + 'myads', { params });

        for (let i = 0; i < request.take; i++) {
            let dummyItem: GetMyAdsListItem = {
                description: `The dummy description ${i}`,
                id: `${i}`,
                publishedOn: Date.now().toString(),
                title: `The Dummy title ${i} from page ${this.getSelectedPage(request.skip, request.take)}`,
                updatedOn: Date.now().toString()
            };

            items.push(dummyItem);
        }

        let response: GetMyAdsListResponse = { count: 110, items: items };

        return of(response);
    }

    getSelectedPage(skip: number, take: number) {
        let selectedPageNumber = Math.ceil((skip + 1) / take);
        selectedPageNumber = selectedPageNumber > 0 ? selectedPageNumber : 1;

        return selectedPageNumber;
    }
}
