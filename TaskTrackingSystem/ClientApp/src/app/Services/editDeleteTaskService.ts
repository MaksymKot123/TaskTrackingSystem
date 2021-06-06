import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { HeadersService } from "./headersService";


@Injectable({
  providedIn: "root"
})
export class EditDeleteTaskService {
  constructor(private http: HttpClient, private headers: HeadersService) { }


  editTask(url: string, projName: string, taskName: string, description: string,
    status: string, startTime: string, endTime: string) {
    const body = {
      "TaskName": taskName,
      "ProjName": projName,
      "StartTime": startTime,
      "EndTIme": endTime,
      "Status": status,
      "Description": description
    };

    return this.http.put(url, body, { headers: this.headers.getHeaders() });
  }

  deleteTask(url: string, projName: string, taskName: string, description: string,
    status: string, startTime: string, endTime: string) {

    const body = {
      "TaskName": taskName,
      "ProjName": projName,
      "StartTime": startTime,
      "EndTime": endTime,
      "Status": status,
      "Description": description
    };

    return this.http.request("delete", url, {
      body: body,
      headers: this.headers.getHeaders()
    });
  }
}
