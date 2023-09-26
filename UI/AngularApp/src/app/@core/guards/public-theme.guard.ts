import { inject } from '@angular/core';
import { CanActivateFn, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '@core/services/auth.service';

export const publicThemetGuard: CanActivateFn = (
    route: ActivatedRouteSnapshot, state: RouterStateSnapshot
) => {
    const authService: AuthService = inject(AuthService);
    const router: Router = inject(Router);

    if (!authService.isAuthenticated())
        return true;

    router.navigate(['app']);

    return false;
};
