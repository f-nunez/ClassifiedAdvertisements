import { Role } from "@core/enums/role";

export interface UserInfo {
    email: string;
    id: string;
    name: string;
    roles: Role[]
    username: string;
}
