import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";

const token = localStorage.getItem("access_token");
const headers = new HttpHeaders({
  'Content-Type': 'application/json',
  'Authorization': `Bearer ${token}`
});

@Injectable({
  providedIn: "root"
})
export class DeleteUserService {
  constructor(private http: HttpClient) { }

  delete(url: string, email: string) {
    const body = {
      "Email": email,
      "Name": "name"
    };

    return this.http.request("delete", url, {
      body: body,
      headers: headers
    });
  }
}
