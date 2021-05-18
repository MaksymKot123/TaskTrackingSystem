import { Component, OnInit } from '@angular/core';
import { GetUsersByRoleService } from "src/app/Services/getUsersByRoleService";
import { IUser } from '../Interfaces/iuser';

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

  constructor(private usrService: GetUsersByRoleService) { }

  admins: IUser[] = [];
  employees: IUser[] = [];
  managers: IUser[] = [];


  ngOnInit() {
    this.getAdmins();
    this.getManagers();
    this.getEmployees();
  }

  getAdmins() {
    this.usrService.getUsers(URL, ADMIN).subscribe(x => this.admins = x);
  }

  getManagers() {
    this.usrService.getUsers(URL, MANAGER).subscribe(x => this.managers = x);
  }

  getEmployees() {
    this.usrService.getUsers(URL, EMPLOYEE).subscribe(x => this.employees = x);
  }



  a() {
    
    console.log(this.admins);
    console.log(this.employees);
    console.log(this.managers);
  }


}
