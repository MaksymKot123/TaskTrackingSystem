import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient, HttpClientModule, HttpParams } from "@angular/common/http";

const URL = "https://localhost:44351/register/";

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styles: [`
    input.ng-touched.ng-invalid {border:solid red 2px;}
    input.ng-touched.ng-valid {border:solid green 2px;}
  `],
})
export class RegisterComponent {
  //email: String = "";
  //password: String = "";
  //name: String = "";
  myForm: FormGroup;

  constructor(private http: HttpClient) {
    this.myForm = new FormGroup({
      "name": new FormControl("", Validators.required),
      "email": new FormControl("", [
        Validators.required,
        Validators.email
      ]),
      "password": new FormControl("", Validators.required)
    });
  }

  submit() {
    //console.log(this.myForm);


    let name = this.myForm.controls["name"].value;
    let email = this.myForm.controls["email"].value;
    let password = this.myForm.controls["password"].value;

    let body = { "Email": email, "Name": name, "Password": password };

    let res = this.http.post(URL, body).toPromise();//.then(res => console.log(res)).catch(console.log);
    console.log(res);
  }

}
