import { Injectable } from '@angular/core';
import { Role } from '@core/enums/role';
import { RoleHelper } from '@core/helpers/role.helper';
import { AuthContext } from '@core/models/auth-context';
import { UserInfo } from '@core/models/user-info';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private authContext: AuthContext = {};

    constructor(private roleHelper: RoleHelper) { }

    public getUserRoles(): Role[] {
        var roles: Role[] = [];

        if (!!this.authContext.userInfo && !!this.authContext.userInfo.roles)
            roles = this.authContext.userInfo.roles;

        return roles;
    }

    public haveAccessToAnyRoles(roles: Role[]): boolean {
        return this.roleHelper.existsAnyRoles(this.getUserRoles(), roles);
    }

    public isAuthenticated(): boolean {
        return !!this.authContext.userInfo;
    }

    public async signinAsync(): Promise<void> {
        this.mockLogin();

        return await new Promise(resolve => resolve());
    }

    public async signoutAsync(): Promise<void> {
        this.mockLogout();

        return await new Promise(resolve => resolve());
    }

    private mockLogin(): void {
        var roles: Role[] = [Role.Customer, Role.Manager, Role.Staff];

        var userInfo: UserInfo = {
            id: '123456789',
            name: 'my name',
            roles: roles
        };

        var authContext: AuthContext = {
            userInfo: userInfo
        };

        this.authContext = authContext;
    }

    private mockLogout(): void {
        this.authContext = {};
    }
}
