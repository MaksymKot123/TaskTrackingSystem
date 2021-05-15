import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { IProject } from "../Interfaces/iproject";
import { Observable } from "rxjs";
import { map } from "rxjs/operators"; 

@Injectable({
  providedIn: "root",
})
export class GetAllProjectsService {
  constructor(private http: HttpClient) { }

  getAllProjects(url: string): Observable<IProject[]> {
    const token = localStorage.getItem("access_token");
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });

    return this.http.get<IProject[]>(url, { headers: headers }).pipe(map(data => {
      let arr = data;
      return arr.map(function (proj: IProject) {
        let res: IProject = {
          name: proj.name,
          status: proj.status,
          clientEmail: proj.clientEmail,
          startTime: proj.startTime,
          endTime: proj.endTime,
          description: proj.description,
          percentCompletion: proj.percentCompletion,
        };

        return res;
      })
    }));
  }
}