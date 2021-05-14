import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
  providedIn: "root",
})
export class GetAllProjectsService {
  constructor(private http: HttpClient) { }

  getAllProjects(url: string) {
    const token = localStorage.getItem("access_token");
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });

    return this.http.get(url, { headers: headers });
  }
}
