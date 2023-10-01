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
        // items = httpClient.get<GetMyAdsListResponse>('apiQueryUrl' + 'myads', { params });

        for (let i = request.skip; i < request.skip + request.take; i++) {
            let dummyItem: GetMyAdsListItem = {
                description: `The dummy description ${i}`,
                id: `${i}`,
                publishedOn: Date.now().toString(),
                title: `The Dummy title ${i}`,
                updatedOn: Date.now().toString()
            };

            items.push(dummyItem);
        }

        let response: GetMyAdsListResponse = { count: 10, items: items };

        return of(response);
    }
}
