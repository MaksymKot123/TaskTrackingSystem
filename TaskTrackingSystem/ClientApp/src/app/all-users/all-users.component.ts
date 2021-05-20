import { Component, OnInit } from '@angular/core';
import { GetUsersByRoleService } from "src/app/Services/getUsersByRoleService";
import { IProject } from '../Interfaces/iproject';
import { IUser } from '../Interfaces/iuser';
import { GetProjectsOfUserService } from '../Services/getProjectsOfUser';


const URL = "https://localhost:44351/account";


const ADMIN = "Admin";
const MANAGER = "Manager";
const EMPLOYEE = "Employee";

@Component({
  selector: 'app-all-users',
  templateUrl: './all-users.component.html',
  styleUrls: ['./all-users.component.css']
})
export class AllUsersComponent implements OnInit {

  constructor(private usrService: GetUsersByRoleService,
    private projService: GetProjectsOfUserService) { }

  admins: IUser[] = [];
  employees: IUser[] = [];
  managers: IUser[] = [];
  error: any;

  //projects: IProject[] = [];



  ngOnInit() {
    this.getAdmins();
    this.getManagers();
    this.getEmployees();
  }

  getAdmins() {
    this.usrService.getUsers(URL, ADMIN).subscribe(x => this.admins = x,
      error => { this.error = error });
  }

  getManagers() {
    this.usrService.getUsers(URL, MANAGER).subscribe(x => this.managers = x,
      error => { this.error = error });
  }

  getEmployees() {
    this.usrService.getUsers(URL, EMPLOYEE).subscribe(x => this.employees = x,
      error => { this.error = error });
  }

  toogle(model: { val: boolean }) {
    model.val = !model.val;
  }
}
