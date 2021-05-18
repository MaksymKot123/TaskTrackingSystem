import { Component } from "@angular/core";
import { Router } from "@angular/router";

@Component({
  selector: "manager",
  templateUrl: "./manager.component.html",
})
export class ManagerComponent {
  constructor(private router: Router) {
    if (localStorage.getItem("role") !== "Manager") {
      this.router.navigateByUrl("/");
    }
  }

  signOut() {
    localStorage.clear();
    this.router.navigateByUrl("");
  }

  showEmployees() {
    this.router.navigateByUrl("manager/users");
  }

  showProject() {
    this.router.navigateByUrl("manager");
  }
}
