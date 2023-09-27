import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class JwtHelper {
    structureSeparator: string = '.';
    payloadPosition: number = 1;

    public getClaims(accessToken: string): any {
        var jwtPayload = this.getPayload(accessToken);

        var payloadMap = new Map(Object.entries(jwtPayload));

        for (const key of payloadMap.keys()) {
            console.log("key: " + key + ", value:" + payloadMap.get(key));
        }

        return jwtPayload;
    }

    public getPayload(accessToken: string): any {
        var partsOfAccessToken = accessToken.split(this.structureSeparator);

        var encodedPayload = partsOfAccessToken[this.payloadPosition];

        var decodedJwtPayload = window.atob(encodedPayload);

        var jwtPayload = JSON.parse(decodedJwtPayload);

        return jwtPayload;
    }
}