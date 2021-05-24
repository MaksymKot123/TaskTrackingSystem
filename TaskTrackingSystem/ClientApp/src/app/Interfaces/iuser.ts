export interface IUser {
  name: string;
  email: string;
  role: string;
  addToProject: { val: boolean };
  viewProjects: { val: boolean };
  changeRole: { val: boolean };
}
