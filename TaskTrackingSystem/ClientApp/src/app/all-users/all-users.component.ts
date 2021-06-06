import { Component, OnInit } from '@angular/core';
import { GetUsersByRoleService } from "src/app/Services/getUsersByRoleService";
import { IUser } from '../Interfaces/iuser';
import { DeleteUserService } from '../Services/deleteuserservice';
import { GetProjectsOfUserService } from '../Services/getProjectsOfUser';


const URL = "https://localhost:44351/account";


const Admin = "Admin";
const Manager = "Manager";
const Employee = "Employee";

@Component({
  selector: 'app-all-users',
  templateUrl: './all-users.component.html'
})
export class AllUsersComponent implements OnInit {

  constructor(private usrService: GetUsersByRoleService,
    private projService: GetProjectsOfUserService,
    private delUsersServ: DeleteUserService) {
    this.currentUserEmail = localStorage.getItem("email");
  }

  admins: IUser[] = [];
  employees: IUser[] = [];
  managers: IUser[] = [];
  error: any;

  currentUserEmail: string;

  ngOnInit() {
    this.getAdmins();
    this.getManagers();
    this.getEmployees();
  }

  getAdmins() {
    this.usrService.getUsers(URL, Admin).subscribe(x => this.admins = x,
      error => { this.error = error });
  }

  getManagers() {
    this.usrService.getUsers(URL, Manager).subscribe(x => this.managers = x,
      error => { this.error = error });
  }

  getEmployees() {
    this.usrService.getUsers(URL, Employee).subscribe(x => this.employees = x,
      error => { this.error = error });
  }

  toogle(model: { val: boolean }) {
    model.val = !model.val;
  }

  deleteUser(email: string) {
    let wantToDelete = confirm("Do you want to delete this user?");
    if (wantToDelete) {
      this.delUsersServ.delete(URL, email).subscribe();
    }
  }
}
