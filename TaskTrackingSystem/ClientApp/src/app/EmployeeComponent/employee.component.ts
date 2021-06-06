import { HttpHeaders } from "@angular/common/http";
import { Route } from "@angular/compiler/src/core";
import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { IProject } from "../Interfaces/iproject";
import { GetEmployeesProjectService } from "../Services/getEmployeesProject";

const URL = "https://localhost:44351/project";
const Email = localStorage.getItem("email");

@Component({
  selector: "employee",
  templateUrl: "./employee.component.html",
})

export class EmployeeComponent implements OnInit {
  constructor(private router: Router, private serv: GetEmployeesProjectService) {
    if (localStorage.getItem("role") !== "Employee") {
      this.router.navigateByUrl("not-found");
    }
  }
  error: any;

  ngOnInit() {
    this.showProject();
  }

  projects: IProject[] = [];

  showProject() {
    this.serv.getProjects(URL, Email).subscribe(x => this.projects = x,
      error => { this.error = error });
  }

  signOut() {
    localStorage.clear();
    this.router.navigateByUrl("");
  }

  toogle(model: { val: boolean }) {
    model.val = !model.val;
  }
}
