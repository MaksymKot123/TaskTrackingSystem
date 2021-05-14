import { Component } from "@angular/core";
import { FormGroup, FormControl, Validator, Validators } from "@angular/forms";
import { NgModule } from "@angular/core";
import { OnInit } from "@angular/core";
import { JwtAuthService } from "../Services/jwtAuth.service";

const URL = "https://localhost:44351/account/login";

@Component({
  selector: "login",
  templateUrl: "./login.component.html"
})

export class LoginComponent implements OnInit {


  myForm: FormGroup;

  constructor(private jwtService: JwtAuthService) { }

  ngOnInit() {
    this.myForm = new FormGroup({
      "email": new FormControl("", [
        Validators.required,
        Validators.email
      ]),
      "password": new FormControl("", Validators.required),
      "confirmPassword": new FormControl("", Validators.required)
    })
  }

  submit() {
    let email = this.myForm.controls["email"].value;
    let password = this.myForm.controls["password"].value;
    let token: string;

    this.jwtService.getToken(URL, email, password).subscribe((data: string) => {
      token = data;
      localStorage.setItem("access_token", token);
      console.log(token);
    });
    
  }






}
