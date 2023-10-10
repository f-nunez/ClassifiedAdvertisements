import { Injectable } from '@angular/core';
import { Observable, delay, of } from 'rxjs';
import { GetMyAdsListItem } from '../interfaces/my-ads-list/get-my-ads-list-item';
import { GetMyAdsListRequest } from '../interfaces/my-ads-list/get-my-ads-list-request';
import { GetMyAdsListResponse } from '../interfaces/my-ads-list/get-my-ads-list-response';
import { CreateMyAdRequest } from '../interfaces/create-my-ad/create-my-ad-request';
import { CreateMyAdResponse } from '../interfaces/create-my-ad/create-my-ad-response';
import { GetMyAdUpdateResponse } from '../interfaces/get-my-ad-update/get-my-ad-update-response';
import { GetMyAdUpdateRequest } from '../interfaces/get-my-ad-update/get-my-ad-update-request';
import { GetMyAdUpdateItem } from '../interfaces/get-my-ad-update/get-my-ad-update-item';
import { UpdateMyAdRequest } from '../interfaces/update-my-ad/update-my-ad-request';
import { UpdateMyAdResponse } from '../interfaces/update-my-ad/update-my-ad-response';
import { GetMyAdDetailItem } from '../interfaces/get-my-ad-detail/get-my-ad-detail-item';
import { GetMyAdDetailRequest } from '../interfaces/get-my-ad-detail/get-my-ad-detail-request';
import { GetMyAdDetailResponse } from '../interfaces/get-my-ad-detail/get-my-ad-detail-response';
import { DeleteMyAdRequest } from '../interfaces/delete-my-ad/delete-my-ad-request';
import { DeleteMyAdResponse } from '../interfaces/delete-my-ad/delete-my-ad-response';

@Injectable()
export class MyAdsService {
    private items: GetMyAdsListItem[] = [];

    constructor() {
        this.populateMockedData();
    }

    public createMyAd(request: CreateMyAdRequest): Observable<CreateMyAdResponse> {
        let response = this.createMockedAd(request);

        return of(response).pipe(delay(500));
    }

    public deleteMyAd(request: DeleteMyAdRequest): Observable<DeleteMyAdResponse> {
        let response = this.deleteMockedAd(request);

        return of(response).pipe(delay(500));
    }

    public getMyAdDetail(request: GetMyAdDetailRequest): Observable<GetMyAdDetailResponse> {
        let response = this.getMockedAdDetail(request);

        return of(response).pipe(delay(500));
    }

    public getMyAdUpdate(request: GetMyAdUpdateRequest): Observable<GetMyAdUpdateResponse> {
        let response = this.getMockedAdUpdate(request);

        return of(response).pipe(delay(500));
    }

    public getMyAdsList(request: GetMyAdsListRequest): Observable<GetMyAdsListResponse> {
        // TODO: http request to the query api
        // let params = new HttpParams();
        // params = params.append('skip', request.skip);
        // params = params.append('take', request.take);
        // response = httpClient.get<GetMyAdsListResponse>('apiQueryUrl' + 'myads', { params });

        let response = this.getMockedList(request);

        return of(response).pipe(delay(500));
    }

    public updateMyAd(request: UpdateMyAdRequest): Observable<UpdateMyAdResponse> {
        let response = this.updateMockedAd(request);

        return of(response).pipe(delay(500));
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

    private deleteMockedAd(request: DeleteMyAdRequest): DeleteMyAdResponse {
        this.items = this.items.filter(obj => obj.id !== request.id);

        return {};
    }

    private getMockedAdDetail(request: GetMyAdDetailRequest): GetMyAdDetailResponse {
        var foundItem: GetMyAdDetailItem = {
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
        if (!!request.sortColumn) {
            switch (request.sortColumn) {
                case 'title':
                    if (request.sortAscending)
                        this.items.sort((a, b) => a.title.localeCompare(b.title, undefined, { numeric: true, sensitivity: 'base' }));
                    else
                        this.items.sort((a, b) => b.title.localeCompare(a.title, undefined, { numeric: true, sensitivity: 'base' }));
                    break;
                case 'description':
                    if (request.sortAscending)
                        this.items.sort((a, b) => a.description.localeCompare(b.description, undefined, { numeric: true, sensitivity: 'base' }));
                    else
                        this.items.sort((a, b) => b.description.localeCompare(a.description, undefined, { numeric: true, sensitivity: 'base' }));
                    break;
                case 'publishedOn':
                    if (request.sortAscending)
                        this.items.sort((a, b) => a.publishedOn.localeCompare(b.publishedOn, undefined, { numeric: true, sensitivity: 'base' }));
                    else
                        this.items.sort((a, b) => b.publishedOn.localeCompare(a.publishedOn, undefined, { numeric: true, sensitivity: 'base' }));
                    break;
                case 'updatedOn':
                    if (request.sortAscending)
                        this.items.sort((a, b) => a.updatedOn.localeCompare(b.updatedOn, undefined, { numeric: true, sensitivity: 'base' }));
                    else
                        this.items.sort((a, b) => b.updatedOn.localeCompare(a.updatedOn, undefined, { numeric: true, sensitivity: 'base' }));
                    break;
                default:
                    break;
            }
        }

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
            let date = new Date();
            date.setDate(date.getDate() + (i - totalFakeItems));
            let dummyItem: GetMyAdsListItem = {
                description: `The dummy description ${i}`,
                id: `${i}`,
                publishedOn: date.getTime().toString(),
                title: `The Dummy title ${i}`,
                updatedOn: date.getTime().toString(),
                version: 0
            };

            this.items.push(dummyItem);
        }
    }

    private updateMockedAd(request: UpdateMyAdRequest): UpdateMyAdResponse {
        this.items.forEach(item => {
            if (item.id == request.id) {
                item.description = request.description;
                item.publishedOn = Date.now().toString();
                item.title = request.title;
                item.updatedOn = Date.now().toString();
                item.version++;
                return;
            }
        });

        return {};
    }
}
