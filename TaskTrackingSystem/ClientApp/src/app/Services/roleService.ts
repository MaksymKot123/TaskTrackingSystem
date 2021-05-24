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
export class RoleService {
  constructor(private http: HttpClient) { }

  changeRole(url: string, usersEmail: string, roleName: string) {
    const body = { "email": usersEmail };

    return this.http.put(url, body, {
      headers: headers,
      params: new HttpParams().set("roleName", roleName),
    });
  }
}
