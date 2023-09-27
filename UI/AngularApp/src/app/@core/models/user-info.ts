import { Role } from "@core/enums/role";

export interface UserInfo {
    id: string;
    name: string;
    roles: Role[]
}
