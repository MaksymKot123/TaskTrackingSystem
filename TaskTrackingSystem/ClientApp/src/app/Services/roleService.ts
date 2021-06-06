import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { HeadersService } from "./headersService";


@Injectable({
  providedIn: "root"
})
export class RoleService {
  constructor(private http: HttpClient, private headers: HeadersService) { }

  changeRole(url: string, usersEmail: string, roleName: string) {
    const body = { "email": usersEmail };

    return this.http.put(url, body, {
      headers: this.headers.getHeaders(),
      params: new HttpParams().set("roleName", roleName),
    });
  }
}
