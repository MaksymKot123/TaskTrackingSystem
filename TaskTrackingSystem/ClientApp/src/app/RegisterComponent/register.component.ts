import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient, HttpClientModule, HttpParams } from "@angular/common/http";
import { RegistrationService } from "../Services/registration.service";
import { OnInit } from "@angular/core";

const URL = "https://localhost:44351/account";

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styles: [`
    input.ng-touched.ng-invalid {border:solid red 2px;}
    input.ng-touched.ng-valid {border:solid green 2px;}
  `],
})
export class RegisterComponent implements OnInit {

  myForm: FormGroup;
  error: any;

  constructor(private regServ: RegistrationService) { }

  ngOnInit() {
    this.myForm = new FormGroup({
      "name": new FormControl("", Validators.required),
      "email": new FormControl("", [
        Validators.required,
        Validators.email
      ]),
      "password": new FormControl("", Validators.required),
      "confirmPassword": new FormControl("", Validators.required)
    });
  }

  submit() {
    let name = this.myForm.controls["name"].value;
    let email = this.myForm.controls["email"].value;
    let password = this.myForm.controls["password"].value;
    let confirmPassword = this.myForm.controls["confirmPassword"].value;
    if (password === confirmPassword) {
      let res = this.regServ.postData(email, password, name, URL)
        .subscribe(null, error => {
          console.log(error);
          this.error = error;
        });
    }
  }
}
