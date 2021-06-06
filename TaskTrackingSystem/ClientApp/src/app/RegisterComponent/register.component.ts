import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RegistrationService } from "../Services/registration.service";

const URL = "https://localhost:44351/account";

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
})
export class RegisterComponent implements OnInit {

  myForm: FormGroup;
  error: any;
  registered: boolean;

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
      this.regServ.postData(email, password, name, URL)
        .subscribe(() => { this.registered = true }, error => {
          console.log(error);
          this.error = error;
        });
    }
  }
}
