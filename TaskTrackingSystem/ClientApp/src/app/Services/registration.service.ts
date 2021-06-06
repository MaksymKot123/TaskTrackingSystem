import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class RegistrationService {

  constructor(private http: HttpClient) { }

  postData(email: string, password: string, name: string, url: string) {
    const body = { "Email": email, "Name": name, "Password": password };
    return this.http.post(url, body);
  }
}
