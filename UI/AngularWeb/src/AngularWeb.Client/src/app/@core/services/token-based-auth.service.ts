import { Injectable } from '@angular/core';
import { Role } from '@core/enums/role';
import { AuthContextHelper } from '@core/helpers/auth-context.helper';
import { RoleHelper } from '@core/helpers/role.helper';
import { AuthContext } from '@core/interfaces/auth-context';
import { UserInfo } from '@core/interfaces/user-info';
import { UserManager, User } from 'oidc-client-ts';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class TokenBasedAuthService {
    private authContext: AuthContext = {};
    private isAuthenticated$ = new BehaviorSubject<boolean>(false);
    private user: User | null = null;
    private userManager: UserManager;

    constructor(
        private authContextHelper: AuthContextHelper,
        private roleHelper: RoleHelper
    ) {
        const userManagerSettings = {
            authority: environment.oidcSetting.authority,
            automaticSilentRenew: environment.oidcSetting.automaticSilentRenew,
            client_id: environment.oidcSetting.clientId,
            post_logout_redirect_uri: environment.oidcSetting.postLogoutRedirectUri,
            redirect_uri: environment.oidcSetting.redirectUri,
            response_type: environment.oidcSetting.responseType,
            scope: environment.oidcSetting.scope,
            silent_redirect_uri: environment.oidcSetting.silentRedirectUri
        };

        this.userManager = new UserManager(userManagerSettings);

        if (environment.useFakeAuth)
            return;

        if (environment.oidcSetting.automaticSilentRenew)
            this.setUserToAuthContextWithSilentRenew();
        else
            this.setUserToAuthContext();

        this.setEventsForUserManager();
    }

    public haveAccessToAnyRoles(roles: Role[]): boolean {
        if (!this.authContext.userInfo || !this.authContext.userInfo.roles)
            return false;

        const userRoles = this.authContext.userInfo.roles;
        return this.roleHelper.existsAnyRoles(userRoles, roles);
    }

    public isAuthenticated(): boolean {
        if (environment.useFakeAuth)
            return !!this.authContext.userInfo;
        else
            return this.validateUser(this.user);
    }

    public getAuthenticatedObservable(): Observable<boolean> {
        return this.isAuthenticated$.asObservable();
    }

    private setAuthenticatedObservable(isAuth: boolean): void {
        this.isAuthenticated$.next(isAuth);
    }

    public async signinAsync(): Promise<void> {
        if (environment.useFakeAuth)
            this.mockLogin();
        else
            return await this.userManager.signinRedirect();
    }

    public async signinCallbackAsync() {
        return await this.userManager.signinCallback();
    }

    public async signoutAsync(): Promise<void> {
        if (environment.useFakeAuth)
            this.mockLogout();
        else
            return await this.userManager.signoutRedirect();
    }

    public getUserUsername(): string {
        if (this.authContext.userInfo)
            return this.authContext.userInfo.username;
        else
            return '';
    }

    public getAccessToken(): string | null {
        if (this.user?.access_token)
            return this.user?.access_token;

        return null;
    }

    private setEventsForUserManager(): void {
        this.userManager.events.addUserLoaded(user => {
            this.setUserToAuthContext();
        });

        this.userManager.events.addUserUnloaded(() => {
            this.setUserToAuthContext();
        });

        this.userManager.events.addUserSessionChanged(() => {
            this.setUserToAuthContext();
        });

        this.userManager.events.addUserSignedIn(() => {
            this.setUserToAuthContext();
        })

        this.userManager.events.addUserSignedOut(() => {
            this.setUserToAuthContext();
        });
    }

    private setUserToAuthContext(): void {
        this.userManager.getUser().then(user => {
            this.validateUserResponse(user);
            this.setAuthenticatedObservable(this.isAuthenticated());
        });
    }

    private setUserToAuthContextWithSilentRenew(): void {
        this.userManager.getUser().then(user => {
            this.validateUserResponse(user);

            if (!this.isAuthenticated())
                this.useSilentRefresh()
            else
                this.setAuthenticatedObservable(this.isAuthenticated());
        });
    }

    private useSilentRefresh(): void {
        this.userManager.signinSilent().then(user => {
            this.validateUserResponse(user);
            this.setAuthenticatedObservable(this.isAuthenticated());
        });
    }

    private validateUser(user: User | null): boolean {
        return !!user && !!user.access_token && !user.expired;
    }

    private validateUserResponse(user: User | null): void {
        if (this.validateUser(user)) {
            this.user = user;
            this.authContext = this.authContextHelper
                .getAuthContext(this.user!.access_token!);
        } else {
            this.user = null;
            this.authContext = {};
        }
    };

    private mockLogin(): void {
        var roles: Role[] = [Role.Customer, Role.Manager, Role.Staff];

        var userInfo: UserInfo = {
            email: 'test@nunez.ninja',
            id: '123456789',
            name: 'my name',
            roles: roles,
            username: 'theUsername'
        };

        var authContext: AuthContext = {
            claims: [],
            userInfo: userInfo
        };

        this.authContext = authContext;
        this.setAuthenticatedObservable(this.isAuthenticated());
    }

    private mockLogout(): void {
        this.authContext = {};
        this.setAuthenticatedObservable(this.isAuthenticated());
    }
}
