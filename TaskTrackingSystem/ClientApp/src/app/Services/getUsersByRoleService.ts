import { HttpClient, HttpParams, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IUser } from "../Interfaces/iuser";
import { map } from "rxjs/operators";

const token = localStorage.getItem("access_token");
const headers = new HttpHeaders({
  'Content-Type': 'application/json',
  'Authorization': `Bearer ${token}`
});

@Injectable({
  providedIn: "root"
})
export class GetUsersByRoleService {
  constructor(private http: HttpClient) { }

  users: IUser[];

  getUsers(url: string, roleName: string) {
    const params = new HttpParams().set("roleName", roleName);

    return this.http.get<IUser[]>(url, {
      headers: headers,
      params: params
    }).pipe(map(data => {
      let arr = data;
      return arr.map(function (user: IUser) {
        let res: IUser = {
          name: user.name,
          email: user.email,
          role: user.role,
        };

        return res;
      })
    }));
  }
}
