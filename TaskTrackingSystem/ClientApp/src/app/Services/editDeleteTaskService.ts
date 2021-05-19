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
export class EditDeleteTaskService {
  constructor(private http: HttpClient) { }

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

    return this.http.put(url, body, { headers: headers });
  }

  deleteTask(url: string, projName: string, taskName: string, description: string,
    status: string, startTime: string, endTime: string) {
    const params = new HttpParams()
      .set("TaskName", taskName)
      .set("ProjName", projName)
      .set("StartTime", startTime)
      .set("EndTime", endTime)
      .set("Status", status)
      .set("Description", description);

    return this.http.delete(url, {
      headers: headers,
      params: params
    });
  }
}
