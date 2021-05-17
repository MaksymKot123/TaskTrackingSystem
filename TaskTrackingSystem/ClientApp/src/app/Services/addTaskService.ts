import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";

const token = localStorage.getItem("access_token");
const headers = new HttpHeaders({
  'Content-Type': 'application/json',
  'Authorization': `Bearer ${token}`
});

@Injectable({
  providedIn: "root",
})
export class AddNewTaskService {
  constructor(private http: HttpClient) { }

  addNewTask(url: string, projName: string, taskName: string, deadline: string,
    description: string) {
    const body = {
      "TaskName": taskName,
      "EndTime": deadline,
      "Description": description,
      "ProjName": projName,
    };

    return this.http.post(url, body, { headers: headers });
  }
}
