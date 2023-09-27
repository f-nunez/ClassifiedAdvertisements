import { Injectable } from '@angular/core';
import { Role } from '@core/enums/role';

@Injectable({
    providedIn: 'root'
})
export class RoleHelper {
    public existsAnyRoles(rolesA: Role[], rolesB: Role[]): boolean {
        if (!rolesA?.length || !rolesB?.length)
            return false;

        // Find any common element with O(n) complexity
        const setRolesA = new Set(rolesA);

        for (let num of rolesB)
            if (setRolesA.has(num))
                return true;

        return false;
    }
}
