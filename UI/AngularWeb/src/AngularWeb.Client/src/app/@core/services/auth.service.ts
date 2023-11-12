import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Role } from '@core/enums/role';
import { AuthContextHelper } from '@core/helpers/auth-context.helper';
import { RoleHelper } from '@core/helpers/role.helper';
import { AuthContext } from '@core/interfaces/auth-context';
import { Claim } from '@core/interfaces/claim';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
// TODO: load AuthService by interface based on authentication approach
// eg. CookieBasedAuthService implements IAuthService
// eg. TokenBasedAuthService implements IAuthService
// and implement on related modules/features injecting IAuthService on constructors
// or build a resolver constructor. any options sounds good to provide dynamic auth service
export class AuthService {
    private authContext: AuthContext = {};
    private isAuthenticated$ = new BehaviorSubject<boolean>(false);

    constructor(
        private httpClient: HttpClient,
        private authContextHelper: AuthContextHelper,
        private roleHelper: RoleHelper
    ) {
        this.setAuthContext();
    }

    public haveAccessToAnyRoles(roles: Role[]): boolean {
        if (!this.authContext.userInfo || !this.authContext.userInfo.roles)
            return false;

        const userRoles = this.authContext.userInfo.roles;
        return this.roleHelper.existsAnyRoles(userRoles, roles);
    }

    public isAuthenticated(): boolean {
        return !!this.authContext.userInfo;
    }

    public getAuthenticatedObservable(): Observable<boolean> {
        return this.isAuthenticated$.asObservable();
    }

    private setAuthenticatedObservable(isAuth: boolean): void {
        this.isAuthenticated$.next(isAuth);
    }

    public async signinAsync(): Promise<void> {
        window.location.href = `${environment.apiUrl}bff/login`;
    }

    public async signinCallbackAsync(): Promise<void> {
        this.setAuthContext();
    }

    public async signoutAsync(): Promise<void> {
        window.location.href = `${environment.apiUrl}bff/logout`;
    }

    public getUserUsername(): string {
        if (this.authContext.userInfo)
            return this.authContext.userInfo.username;
        else
            return '';
    }

    private setAuthContext(): void {
        this.httpClient.get<Claim[]>(`${environment.apiUrl}bff/user`).subscribe((claims) => {
            this.authContext = this.authContextHelper.getAuthContextFromClaims(claims);
            this.setAuthenticatedObservable(this.isAuthenticated());
        });
    }
}
