import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { HeadersService } from "./headersService";


@Injectable({
  providedIn: "root",
})
export class AddNewTaskService {
  constructor(private http: HttpClient, private headers: HeadersService) { }

  addNewTask(url: string, projName: string, taskName: string, deadline: string,
    description: string) {
    const body = {
      "TaskName": taskName,
      "EndTime": deadline,
      "Description": description,
      "ProjName": projName,
    };

    return this.http.post(url, body, { headers: this.headers.getHeaders() });
  }
}
