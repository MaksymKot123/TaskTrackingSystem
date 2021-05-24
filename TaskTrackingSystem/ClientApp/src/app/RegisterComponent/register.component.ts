import { Component } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
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

  constructor(private regServ: RegistrationService, private fb: FormBuilder) { }

  readonly RegEx = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{6,})");

  ngOnInit() {
    this.myForm = this.fb.group({
      "name": ["", Validators.required],
      "email": ["", [Validators.required, Validators.email]],
      "password": ["", [Validators.required, Validators.pattern(this.RegEx)]],
      "confirmPassword": ["", Validators.required],
    })
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
