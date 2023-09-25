import { inject } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from "@angular/router";
import { AuthService } from "@core/services/auth.service";

export const appThemeGuard: CanActivateFn = (
    route: ActivatedRouteSnapshot, state: RouterStateSnapshot
) => {
    const authService: AuthService = inject(AuthService);
    const router: Router = inject(Router);

    if (authService.isAuthenticated())
        return true;

    router.navigate(['']);

    return false;
};
