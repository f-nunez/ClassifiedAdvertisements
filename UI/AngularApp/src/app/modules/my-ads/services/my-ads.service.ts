import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { GetMyAdsListItem } from '../interfaces/my-ads-list/get-my-ads-list-item';
import { GetMyAdsListRequest } from '../interfaces/my-ads-list/get-my-ads-list-request';
import { GetMyAdsListResponse } from '../interfaces/my-ads-list/get-my-ads-list-response';
import { CreateMyAdRequest } from '../interfaces/create-my-ad/create-my-ad-request';
import { CreateMyAdResponse } from '../interfaces/create-my-ad/create-my-ad-response';
import { GetMyAdUpdateResponse } from '../interfaces/get-my-ad-update/get-my-ad-update-response';
import { GetMyAdUpdateRequest } from '../interfaces/get-my-ad-update/get-my-ad-update-request';
import { GetMyAdUpdateItem } from '../interfaces/get-my-ad-update/get-my-ad-update-item';

@Injectable()
export class MyAdsService {
    private items: GetMyAdsListItem[] = [];

    constructor() {
        this.populateMockedData();
    }

    public createMyAd(request: CreateMyAdRequest): Observable<CreateMyAdResponse> {
        let response = this.createMockedAd(request);

        return of(response);
    }

    public getMyAdUpdate(request: GetMyAdUpdateRequest): Observable<GetMyAdUpdateResponse> {
        let response = this.getMockedAdUpdate(request);

        return of(response);
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

    private createMockedAd(request: CreateMyAdRequest): CreateMyAdResponse {
        let newId = this.items.length + 1;

        let newDummyItem: GetMyAdsListItem = {
            description: request.description,
            id: `${newId}`,
            publishedOn: Date.now().toString(),
            title: request.title,
            updatedOn: Date.now().toString(),
            version: 0
        };

        this.items.push(newDummyItem);

        return newDummyItem;
    }

    private getMockedAdUpdate(request: GetMyAdUpdateRequest): GetMyAdUpdateResponse {
        var foundItem: GetMyAdUpdateItem = {
            description: '',
            id: '',
            title: '',
            version: 0
        };

        this.items.forEach(item => {
            if (item.id == request.id) {
                foundItem.description = item.description;
                foundItem.id = item.id;
                foundItem.title = item.title;
                foundItem.version = item.version;
                return;
            }
        });

        return { item: foundItem };
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
