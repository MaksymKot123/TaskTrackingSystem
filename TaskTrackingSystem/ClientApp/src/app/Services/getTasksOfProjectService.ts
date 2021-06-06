import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";
import { ITask } from "../Interfaces/itask";
import { HeadersService } from "./headersService";


@Injectable({
  providedIn: "root"
})
export class GetTasksFromProjectService {
  constructor(private http: HttpClient, private headers: HeadersService) { }

  tasks: ITask[];

  getTasks(url: string, projectName: string) {
    const params = new HttpParams()
      .set("ClientEmail", "text")
      .set("Name", "text")
      .set("Name", projectName);

    return this.http.get<ITask[]>(url, {
      headers: this.headers.getHeaders(),
      params: params
    }).pipe(map(data => {
      let arr = data;
      return arr.map(function (task: ITask) {
        return {
          taskName: task.taskName,
          projName: task.projName,
          description: task.description,
          endTime: task.endTime,
          startTime: task.startTime,
          status: task.status
        }
      })
    }));
  }
}
