import { Component } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { JwtAuthService } from "../Services/jwtAuth.service";
import { JwtParseService } from "../Services/jwtParse.service";

const URL = "https://localhost:44351/account/login";

@Component({
  selector: "login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})

export class LoginComponent implements OnInit {


  myForm: FormGroup;

  constructor(private jwtService: JwtAuthService, private router: Router,
    private jwtParser: JwtParseService) {
    if (localStorage.getItem("role") === 'Admin') {
      this.router.navigateByUrl("admin");
    } else if (localStorage.getItem("role") === "Manager") {
      this.router.navigateByUrl("manager");
    } else if (localStorage.getItem("role") === "Employee") {
      this.router.navigateByUrl("employee");
    }
  }

  error: any;

  ngOnInit() {
    this.myForm = new FormGroup({
      "email": new FormControl("", [
        Validators.required,
        Validators.email
      ]),
      "password": new FormControl("", Validators.required),
    })
  }

  submit() {
    let email = this.myForm.controls["email"].value;
    let password = this.myForm.controls["password"].value;
    let token: string;
    let role: string;
    let name: string;

    localStorage.clear();

    this.jwtService.getToken(URL, email, password).subscribe((data: string) => {
      token = data;
      localStorage.setItem("access_token", token);
      let jsonToken = this.jwtParser.parseJwt(token);

      role = jsonToken.role;
      localStorage.setItem("role", role);
      name = jsonToken.unique_name;
      localStorage.setItem("name", name);
      email = jsonToken.email;
      localStorage.setItem("email", email);

      if (role === 'Admin') {
        this.router.navigateByUrl("admin");
      } else if (role === "Manager") {
        this.router.navigateByUrl("manager");
      } else if (role === "Employee") {
        this.router.navigateByUrl("employee");
      } else {
        this.router.navigateByUrl("");
      }

    }, error => {
        this.error = error;
        console.log(error);
    });
  }






}
