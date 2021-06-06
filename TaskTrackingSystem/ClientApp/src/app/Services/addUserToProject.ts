import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { HeadersService } from "./headersService";

@Injectable({
  providedIn: "root"
})
export class AddUserToProjectService {
  constructor(private http: HttpClient, private headers: HeadersService) { }

  addUser(url: string, projectName: string, userEmail: string) {
    const body = {
      "Email": userEmail,
    };
    const params = new HttpParams().set("projName", projectName);
    return this.http.post(url, body, { headers: this.headers.getHeaders(), params: params });
  }
}
