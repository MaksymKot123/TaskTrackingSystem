import { Route } from "@angular/compiler/src/core";
import { Component } from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: "employee",
  templateUrl: "./employee.component.html",
})
export class EmployeeComponent {
  constructor(private router: Router) {
    if (localStorage.getItem("role") !== "Employee") {
      this.router.navigateByUrl("");
    }

  }
}
