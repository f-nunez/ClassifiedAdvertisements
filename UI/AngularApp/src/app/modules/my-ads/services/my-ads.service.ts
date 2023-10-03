import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { GetMyAdsListItem } from '../interfaces/my-ads-list/get-my-ads-list-item';
import { GetMyAdsListRequest } from '../interfaces/my-ads-list/get-my-ads-list-request';
import { GetMyAdsListResponse } from '../interfaces/my-ads-list/get-my-ads-list-response';

@Injectable()
export class MyAdsService {
    items: GetMyAdsListItem[] = [];

    constructor() {
        this.populateMockedData();
    }

    public getMyAdsList(request: GetMyAdsListRequest): Observable<GetMyAdsListResponse> {
        // TODO: http request to the query api
        // let params = new HttpParams();
        // params = params.append('skip', request.skip);
        // params = params.append('take', request.take);
        // response = httpClient.get<GetMyAdsListResponse>('apiQueryUrl' + 'myads', { params });

        let response = this.getMockedList(request);

        return of(response);
    }

    private getMockedList(request: GetMyAdsListRequest): GetMyAdsListResponse {
        let items: GetMyAdsListItem[] = [];

        for (let i = request.skip; i < (request.skip + request.take); i++) {
            if (i >= this.items.length)
                break;

            items.push(this.items[i]);
        }

        let response: GetMyAdsListResponse = { count: this.items.length, items: items };

        return response;
    }

    private populateMockedData(): void {
        const totalFakeItems = 115;

        for (let i = 1; i <= totalFakeItems; i++) {
            let dummyItem: GetMyAdsListItem = {
                description: `The dummy description ${i}`,
                id: `${i}`,
                publishedOn: Date.now().toString(),
                title: `The Dummy title ${i}`,
                updatedOn: Date.now().toString(),
                version: 0
            };

            this.items.push(dummyItem);
        }
    }
}
