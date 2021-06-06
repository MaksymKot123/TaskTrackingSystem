import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { HeadersService } from "./headersService";

@Injectable({
  providedIn: "root"
})
export class EditProjectService {

  constructor(private http: HttpClient, private headers: HeadersService) { }

  editProject(url: string, name: string, description: string,
    clientEmail: string, endTime: string) {
    const body = {
      "Name": name,
      "Description": description,
      "ClientEmail": clientEmail,
      "EndTime": endTime,
    };

    return this.http.put(url, body, { headers: this.headers.getHeaders() });
  }
}
