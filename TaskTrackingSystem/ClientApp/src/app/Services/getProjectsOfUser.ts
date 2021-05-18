import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IProject } from "../Interfaces/iproject";

const token = localStorage.getItem("access_token");
const headers = new HttpHeaders({
  'Content-Type': 'application/json',
  'Authorization': `Bearer ${token}`
});

@Injectable({
  providedIn: "root"
})
export class GetProjectsOfUserService {
  constructor(private http: HttpClient) { }

  getProjects(url: string, userEmail: string) {
    return this.http.get<IProject[]>(url, {
      headers: headers,
      params: new HttpParams().set("email", userEmail)
    });
  }
}
