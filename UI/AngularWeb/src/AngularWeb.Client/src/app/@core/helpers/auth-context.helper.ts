import { Injectable } from '@angular/core';
import { Role } from '@core/enums/role';
import { AuthContext } from '@core/interfaces/auth-context';
import { Claim } from '@core/interfaces/claim';
import { UserInfo } from '@core/interfaces/user-info';

@Injectable({
    providedIn: 'root'
})
export class AuthContextHelper {
    private jwtSeparator: string = '.';
    private jwtPayloadPosition: number = 1;

    public getAuthContext(accessToken: string): AuthContext {
        let jwtPayload = this.getJwtPayload(accessToken);

        let claims: Claim[] = jwtPayload;
        let email: string = jwtPayload['email'];
        let id: string = jwtPayload['sub'];
        let name: string = jwtPayload['name'];
        let roles: Role[] = jwtPayload['role'];

        if (!Array.isArray(roles))
            roles = [roles];

        let username: string = jwtPayload['preferred_username'];

        let userInfo: UserInfo = {
            email: email,
            id: id,
            name: name,
            roles: roles,
            username: username
        };

        let authContext: AuthContext = {
            claims: claims,
            userInfo: userInfo
        };

        return authContext;
    }

    private getJwtPayload(accessToken: string): any {
        let partsOfAccessToken = accessToken.split(this.jwtSeparator);
        let encodedPayload = partsOfAccessToken[this.jwtPayloadPosition];
        let decodedJwtPayload = window.atob(encodedPayload);
        let jwtPayload = JSON.parse(decodedJwtPayload);

        return jwtPayload;
    }
}
