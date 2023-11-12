import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class TokenProviderService {
    private token: string = '';

    public getAccessToken(): string {
        return this.token;
    }

    public setAccessToken(token: string | undefined): void {
        this.token = token ? token : '';
    }
}
