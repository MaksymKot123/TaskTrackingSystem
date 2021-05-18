import { HttpHeaders } from "@angular/common/http";
import { Route } from "@angular/compiler/src/core";
import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { IProject } from "../Interfaces/iproject";
import { GetEmployeesProjectService } from "../Services/getEmployeesProject";

const URL = "https://localhost:44351/project";
const EMAIL = localStorage.getItem("email");
//const token = localStorage.getItem("access_token");
//const headers = new HttpHeaders({
//  'Content-Type': 'application/json',
//  'Authorization': `Bearer ${token}`
//});

@Component({
  selector: "employee",
  templateUrl: "./employee.component.html",
})

export class EmployeeComponent implements OnInit {
  constructor(private router: Router, private serv: GetEmployeesProjectService) {
    if (localStorage.getItem("role") !== "Employee") {
      this.router.navigateByUrl("");
    }
  }

  ngOnInit() {
    this.showProject();
    console.log(EMAIL);
  }

  projects: IProject[] = [];

  showProject() {
    this.serv.getProjects(URL, EMAIL).subscribe(x => this.projects = x);
  }

  signOut() {
    localStorage.clear();
    this.router.navigateByUrl("");
  }

  toogle(model: { val: boolean }) {
    model.val = !model.val;
  }
}
