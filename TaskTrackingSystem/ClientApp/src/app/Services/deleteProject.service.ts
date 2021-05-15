import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";

const token = localStorage.getItem("access_token");
const headers = new HttpHeaders({
  'Content-Type': 'application/json',
  'Authorization': `Bearer ${token}`
});

@Injectable({
  providedIn: "root"
})
export class DeleteProjectService {
  constructor(private http: HttpClient) { }

  delete(url: string, name: string) {
    const body = {
      "Name": name,
      "ClientEmail": "email",
      "EntTime": Date.now(),
    };
    return this.http.request("delete", url, {
      body: body,
      headers: headers,
    });
  }
}
