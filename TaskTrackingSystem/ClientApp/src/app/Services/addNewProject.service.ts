import { HttpClient, HttpHeaders} from "@angular/common/http";
import { Injectable } from "@angular/core";

const token = localStorage.getItem("access_token");
const headers = new HttpHeaders({
  'Content-Type': 'application/json',
  'Authorization': `Bearer ${token}`
});

@Injectable({
  providedIn: "root",
})
export class AddNewProjectService {
  constructor(private http: HttpClient) { }

  addNewProject(url: string, name: string, deadline: string, clientEmail: string,
    description: string) {

    const body = {
      "ClientEmail": clientEmail,
      "Name": name,
      "EndTime": deadline,
      "Description": description
    };
    return this.http.post(url, JSON.stringify(body), { headers: headers });
  }
}
