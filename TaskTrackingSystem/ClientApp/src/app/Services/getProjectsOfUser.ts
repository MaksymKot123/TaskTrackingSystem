import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IProject } from "../Interfaces/iproject";
import { HeadersService } from "./headersService";

@Injectable({
  providedIn: "root"
})
export class GetProjectsOfUserService {
  constructor(private http: HttpClient, private headers: HeadersService) { }

  getProjects(url: string, userEmail: string) {
    return this.http.get<IProject[]>(url, {
      headers: this.headers.getHeaders(),
      params: new HttpParams().set("email", userEmail)
    });
  }
}
