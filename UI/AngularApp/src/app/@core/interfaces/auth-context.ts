import { Claim } from "./claim";
import { UserInfo } from "./user-info";

export interface AuthContext {
    claims?: Claim[];
    userInfo?: UserInfo;
}
