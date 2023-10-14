import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateMyAdRequest } from '../interfaces/create-my-ad/create-my-ad-request';
import { CreateMyAdResponse } from '../interfaces/create-my-ad/create-my-ad-response';
import { DeleteMyAdRequest } from '../interfaces/delete-my-ad/delete-my-ad-request';
import { DeleteMyAdResponse } from '../interfaces/delete-my-ad/delete-my-ad-response';
import { GetMyAdDetailRequest } from '../interfaces/get-my-ad-detail/get-my-ad-detail-request';
import { GetMyAdDetailResponse } from '../interfaces/get-my-ad-detail/get-my-ad-detail-response';
import { GetMyAdListRequest } from '../interfaces/get-my-ad-list/get-my-ad-list-request';
import { GetMyAdListResponse } from '../interfaces/get-my-ad-list/get-my-ad-list-response';
import { GetMyAdUpdateRequest } from '../interfaces/get-my-ad-update/get-my-ad-update-request';
import { GetMyAdUpdateResponse } from '../interfaces/get-my-ad-update/get-my-ad-update-response';
import { UpdateMyAdRequest } from '../interfaces/update-my-ad/update-my-ad-request';
import { UpdateMyAdResponse } from '../interfaces/update-my-ad/update-my-ad-response';

@Injectable()
export class MyAdsService {
    constructor(private httpClient: HttpClient) {
    }

    public createMyAd(request: CreateMyAdRequest): Observable<CreateMyAdResponse> {
        let apiUrl = 'https://localhost:7200/api/v1/myads';
        return this.httpClient.post<CreateMyAdResponse>(apiUrl, request);
    }

    public deleteMyAd(request: DeleteMyAdRequest): Observable<DeleteMyAdResponse> {
        let apiUrl = `https://localhost:7200/api/v1/myads/${request.id}/version/${request.version}`;
        return this.httpClient.delete<DeleteMyAdResponse>(apiUrl);
    }

    public getMyAdDetail(request: GetMyAdDetailRequest): Observable<GetMyAdDetailResponse> {
        let apiUrl = `https://localhost:7201/api/v1/myads/${request.id}/detail`;
        return this.httpClient.get<GetMyAdDetailResponse>(apiUrl);
    }

    public getMyAdUpdate(request: GetMyAdUpdateRequest): Observable<GetMyAdUpdateResponse> {
        let apiUrl = `https://localhost:7201/api/v1/myads/${request.id}/update`;
        return this.httpClient.get<GetMyAdUpdateResponse>(apiUrl);
    }

    public getMyAdsList(request: GetMyAdListRequest): Observable<GetMyAdListResponse> {
        let params = new HttpParams();
        params = params.append('skip', request.dataTableRequest.skip);
        params = params.append('take', request.dataTableRequest.take);

        if (request.dataTableRequest.sorts && request.dataTableRequest.sorts.length > 0) {
            params = params.append('sortasc', request.dataTableRequest.sorts[0].isAscending);
            params = params.append('sortprop', request.dataTableRequest.sorts[0].propertyName);
        }

        let apiUrl = 'https://localhost:7201/api/v1/myads';
        return this.httpClient.get<GetMyAdListResponse>(apiUrl, { params });
    }

    public updateMyAd(request: UpdateMyAdRequest): Observable<UpdateMyAdResponse> {
        let apiUrl = `https://localhost:7200/api/v1/myads/${request.id}`;
        return this.httpClient.put<UpdateMyAdResponse>(apiUrl, request);
    }
}
