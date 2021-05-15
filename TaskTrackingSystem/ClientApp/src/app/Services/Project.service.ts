import { HttpHeaders, HttpClient } from "@angular/common/http";
import { Injectable, OnInit } from "@angular/core";
import { IProject } from "../Interfaces/iproject";
import { Observable } from "rxjs";
import { map } from "rxjs/operators"; 
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

const URL = "https:localhost:44351/project";

const token = localStorage.getItem("access_token");
const headers = new HttpHeaders({
  'Content-Type': 'application/json',
  'Authorization': `Bearer ${token}`
});

@Injectable({
  providedIn: "root",
})
export class ProjectService implements OnInit {

  myForm: FormGroup;

  constructor(private http: HttpClient, private fb: FormBuilder) { }

  ngOnInit() {
    this.myForm = this.fb.group({

      "endTime": ["", Validators.required],
      "projectName": ["", Validators.required],
      "clientEmail": ["", [Validators.required, Validators.email]],
      "description": [""]
    });
  }

  addNewProject(body: object) {
    return this.http.post(URL, JSON.stringify(body), { headers: headers });
  }

  getAllProjects(url: string): Observable<IProject[]> {
    return this.http.get<IProject[]>(url, { headers: headers }).pipe(map(data => {
      let arr = data;
      return arr.map(function (proj: IProject) {
        let res: IProject = {
          name: proj.name,
          status: proj.status,
          clientEmail: proj.clientEmail,
          startTime: proj.startTime,
          endTime: proj.endTime,
          description: proj.description,
          percentCompletion: proj.percentCompletion,
          editProject: { val: false },
          viewTasks: { val: false },
          deleteProject: { val: false }
        };

        return res;
      })
    }));
  }




}
