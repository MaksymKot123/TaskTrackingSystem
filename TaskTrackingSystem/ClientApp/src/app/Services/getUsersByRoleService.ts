import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IUser } from "../Interfaces/iuser";
import { map } from "rxjs/operators";
import { HeadersService } from "./headersService";


@Injectable({
  providedIn: "root"
})
export class GetUsersByRoleService {
  constructor(private http: HttpClient, private headers: HeadersService) { }

  getUsers(url: string, roleName: string) {
    const params = new HttpParams().set("roleName", roleName);

    return this.http.get<IUser[]>(url, {
      headers: this.headers.getHeaders(),
      params: params
    }).pipe(map(data => {
      let arr = data;
      return arr.map(function (user: IUser) {
        return {
          name: user.name,
          email: user.email,
          role: roleName,
          addToProject: { val: true },
          viewProjects: { val: true },
          changeRole: { val: true },}
      })
    }));
  }
}
