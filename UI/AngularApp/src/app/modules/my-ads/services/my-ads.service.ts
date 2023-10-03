import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { GetMyAdsListItem } from '../interfaces/my-ads-list/get-my-ads-list-item';
import { GetMyAdsListRequest } from '../interfaces/my-ads-list/get-my-ads-list-request';
import { GetMyAdsListResponse } from '../interfaces/my-ads-list/get-my-ads-list-response';

@Injectable()
export class MyAdsService {
    items: GetMyAdsListItem[] = [];

    constructor() {
        // populate fake data
        const totalFakeItems = 115;
        for (let i = 1; i <= totalFakeItems; i++) {
            let dummyItem: GetMyAdsListItem = {
                description: `The dummy description ${i}`,
                id: `${i}`,
                publishedOn: Date.now().toString(),
                title: `The Dummy title ${i}`,
                updatedOn: Date.now().toString()
            };

            this.items.push(dummyItem);
        }
    }

    public getMyAdsList(request: GetMyAdsListRequest): Observable<GetMyAdsListResponse> {
        let items: GetMyAdsListItem[] = [];

        // TODO: http request to the query api
        // let params = new HttpParams();
        // params = params.append('skip', request.skip);
        // params = params.append('take', request.take);
        // response = httpClient.get<GetMyAdsListResponse>('apiQueryUrl' + 'myads', { params });

        for (let i = request.skip; i < (request.skip + request.take); i++) {
            if (i >= this.items.length)
                break;

            items.push(this.items[i]);
        }

        let response: GetMyAdsListResponse = { count: this.items.length, items: items };

        return of(response);
    }

    getSelectedPage(skip: number, take: number) {
        let selectedPageNumber = Math.ceil((skip + 1) / take);
        selectedPageNumber = selectedPageNumber > 0 ? selectedPageNumber : 1;

        return selectedPageNumber;
    }
}
