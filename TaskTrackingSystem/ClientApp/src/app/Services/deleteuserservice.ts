import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { HeadersService } from "./headersService";


@Injectable({
  providedIn: "root"
})
export class DeleteUserService {
  constructor(private http: HttpClient, private headers: HeadersService) { }


  delete(url: string, email: string) {
    const body = {
      "Email": email,
      "Name": "name"
    };

    return this.http.request("delete", url, {
      body: body,
      headers: this.headers.getHeaders()
    });
  }
}
