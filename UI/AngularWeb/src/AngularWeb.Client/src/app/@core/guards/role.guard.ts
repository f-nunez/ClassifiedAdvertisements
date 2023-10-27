import { inject } from '@angular/core';
import { CanActivateFn, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Role } from '@core/enums/role';
import { AuthService } from '@core/services/auth.service';

export const roleGuard: CanActivateFn = (
    route: ActivatedRouteSnapshot, state: RouterStateSnapshot
) => {
    const authService: AuthService = inject(AuthService);
    const router: Router = inject(Router);
    const roles: Role[] = route.data['roles'];

    if (authService.haveAccessToAnyRoles(roles))
        return true;

    router.navigate(['']);

    return false;
};
