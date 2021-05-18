import { Component, OnInit } from '@angular/core';
import { GetUsersByRoleService } from "src/app/Services/getUsersByRoleService";
import { IProject } from '../Interfaces/iproject';
import { IUser } from '../Interfaces/iuser';
import { GetProjectsOfUserService } from '../Services/getProjectsOfUser';


const URL = "https://localhost:44351/account";
const EMPLOYEE = "Employee";

@Component({
  selector: 'all-employees',
  templateUrl: './all-employees.component.html',
  styleUrls: ['./all-employees.component.css']
})
export class AllEmployeesComponent implements OnInit {

  constructor(private usrService: GetUsersByRoleService,
    private projService: GetProjectsOfUserService) { }

  employees: IUser[] = [];

  //projects: IProject[] = [];



  ngOnInit() {
    this.getEmployees();
  }

  getEmployees() {
    this.usrService.getUsers(URL, EMPLOYEE).subscribe(x => this.employees = x);
  }

  toogle(model: { val: boolean }) {
    model.val = !model.val;
  }

}
