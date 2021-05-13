import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject  } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  private loggedIn = new BehaviorSubject<boolean>(false);
  private token: string;

  get isLoggedIn() {
    return this.loggedIn.asObservable();
  }

  //constructor(private router: Router, private s)
}
