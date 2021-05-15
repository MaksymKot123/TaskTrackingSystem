import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

const token = localStorage.getItem("access_token");
const headers = new HttpHeaders({
  'Content-Type': 'application/json',
  'Authorization': `Bearer ${token}`
});

@Injectable({
  providedIn: "root"
})
export class EditProjectService {

  constructor(private http: HttpClient) { }

  editProject(url: string, name: string, description: string,
    clientEmail: string, endTime: string) {
    const body = {
      "Name": name,
      "Description": description,
      "ClientEmail": clientEmail,
      "EndTime": endTime,
    };

    return this.http.put(url, body, { headers: headers });
  }
}
