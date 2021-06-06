import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { HeadersService } from "./headersService";


@Injectable({
  providedIn: "root",
})
export class AddNewProjectService {
  constructor(private http: HttpClient, private headers: HeadersService) { }
  

  addNewProject(url: string, name: string, deadline: string, clientEmail: string,
    description: string) {

    const body = {
      "ClientEmail": clientEmail,
      "Name": name,
      "EndTime": deadline,
      "Description": description
    };
    return this.http.post(url, JSON.stringify(body), { headers: this.headers.getHeaders() });
  }
}
