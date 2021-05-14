import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject  } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class JwtAuthService {
  constructor(private http: HttpClient) { }

  getToken(url: string, email: string, password: string) {
    const body = { Email: email, Password: password };
    return this.http.post(url, body);
  }
}
