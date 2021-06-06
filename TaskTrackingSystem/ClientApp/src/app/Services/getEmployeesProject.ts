import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { IProject } from "../Interfaces/iproject";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { HeadersService } from "./headersService";


@Injectable({
  providedIn: "root"
})
export class GetEmployeesProjectService {
  constructor(private http: HttpClient, private headers: HeadersService) { }

  getProjects(url: string, email: string): Observable<IProject[]> {
    return this.http.get<IProject[]>(url, {
      headers: this.headers.getHeaders(),
      params: new HttpParams().set("email", email)
    }).pipe(map(data => {
      let arr = data;
      return arr.map(function (proj: IProject) {
        return {
          name: proj.name,
          status: proj.status,
          clientEmail: proj.clientEmail,
          startTime: proj.startTime,
          endTime: proj.endTime,
          description: proj.description,
          percentCompletion: Math.round(proj.percentCompletion,),
          editProject: { val: true },
          deleteProject: { val: false },
          addTask: { val: true },
          showTasks: { val: true },
        };
      })
    }));
  }
}
