import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";

const token = localStorage.getItem("access_token");
const headers = new HttpHeaders({
  'Content-Type': 'application/json',
  'Authorization': `Bearer ${token}`
});


@Injectable({
  providedIn: "root"
})
export class AddUserToProjectService {
  constructor(private http: HttpClient) { }

  addUser(url: string, projectName: string, userEmail: string) {
    const body = {
      "Email": userEmail,
    };
    const params = new HttpParams().set("projName", projectName);
    return this.http.post(url, body, { headers: headers, params: params });
  }
}
