import { Role } from "./Role";

export class User {
  name: string;
  email: string;
  role: Role;
  tokem?: string;
}
