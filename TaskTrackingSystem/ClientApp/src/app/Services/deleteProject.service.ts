import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { HeadersService } from "./headersService";


@Injectable({
  providedIn: "root"
})
export class DeleteProjectService {
  constructor(private http: HttpClient, private headers: HeadersService) { }

  

  delete(url: string, name: string) {
    const body = {
      "Name": name,
      "ClientEmail": "email",
      "EntTime": Date.now(),
    };
    return this.http.request("delete", url, {
      body: body,
      headers: this.headers.getHeaders(),
    });
  }
}
