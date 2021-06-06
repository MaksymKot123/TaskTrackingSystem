import { HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class HeadersService  {
  token: string;
  headers: HttpHeaders;

  getHeaders() {
    this.token = localStorage.getItem("access_token");
    this.headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.token}`
    });
    return this.headers;
  }
}
