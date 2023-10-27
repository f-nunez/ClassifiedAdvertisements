import { inject } from '@angular/core';
import { CanActivateFn, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '@core/services/auth.service';

export const publicThemetGuard: CanActivateFn = (
    route: ActivatedRouteSnapshot, state: RouterStateSnapshot
) => {
    const authService: AuthService = inject(AuthService);
    const router: Router = inject(Router);
    let canActivate = false;
    let isAuthenticated$ = authService.getAuthenticatedObservable();

    isAuthenticated$.subscribe(nextResponse => {
        if (nextResponse) {
            router.navigate(['app']);
            canActivate = false;
        } else {
            canActivate = true;
        }

        return canActivate;
    });

    return canActivate;
};
