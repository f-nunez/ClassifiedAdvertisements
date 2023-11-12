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

    public getAuthContextFromClaims(claims: Claim[]): AuthContext {
        let email: string = claims.find(x => x.type == 'email')?.value ?? '';
        let id: string = claims.find(x => x.type == 'sub')?.value ?? '';
        let name: string = claims.find(x => x.type == 'name')?.value ?? '';
        let roles: Role[] = [];

        claims.filter(x => x.type == 'role').forEach(x => {
            var roleEnum: Role = (<any>Role)[x.value];
            roles.push(roleEnum);
        });

        let username: string = claims.find(x => x.type == 'preferred_username')?.value ?? '';

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
