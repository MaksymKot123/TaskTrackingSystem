import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";
import { ITask } from "../Interfaces/itask";

const token = localStorage.getItem("access_token");
const headers = new HttpHeaders({
  'Content-Type': 'application/json',
  'Authorization': `Bearer ${token}`
});

@Injectable({
  providedIn: "root"
})
export class GetTasksFromProjectService {
  constructor(private http: HttpClient) { }

  tasks: ITask[];

  getTasks(url: string, projectName: string) {
    const params = new HttpParams()
      .set("ClientEmail", "text")
      .set("Name", "text")
      .set("Name", projectName);

    return this.http.get<ITask[]>(url, {
      headers: headers,
      params: params
    }).pipe(map(data => {
      let arr = data;
      return arr.map(function (task: ITask) {
        let res: ITask = {
          taskName: task.taskName,
          description: task.description,
          endTime: task.endTime,
          startTime: task.startTime,
          status: task.status
        };

        return res;
      })
    }));
  }
}
