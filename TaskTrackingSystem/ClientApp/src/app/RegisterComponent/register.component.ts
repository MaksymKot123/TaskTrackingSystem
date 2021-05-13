import { Component } from '@angular/core';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
})
export class RegisterComponent {
  email: String = "";
  password: String = "";
  name: String = "";

}
