import { HttpClient } from "@angular/common/http";
import { Injectable, OnInit } from "@angular/core";
import { IProject } from "../Interfaces/iproject";
import { Observable } from "rxjs";
import { map } from "rxjs/operators"; 
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { HeadersService } from "./headersService";

const URL = "https:localhost:44351/project";

@Injectable({
  providedIn: "root",
})
export class ProjectService implements OnInit {

  myForm: FormGroup;

  constructor(private http: HttpClient, private fb: FormBuilder,
    private headers: HeadersService) { }

  ngOnInit() {
    this.myForm = this.fb.group({

      "endTime": ["", Validators.required],
      "projectName": ["", Validators.required],
      "clientEmail": ["", [Validators.required, Validators.email]],
      "description": [""]
    });
  }

  addNewProject(body: object) {
    return this.http.post(URL, JSON.stringify(body), { headers: this.headers.getHeaders() });
  }

  getAllProjects(url: string): Observable<IProject[]> {
    return this.http.get<IProject[]>(url, { headers: this.headers.getHeaders() })
      .pipe(map(data => {
        let arr = data;
        return arr.map(function (proj: IProject) {
          return {
            name: proj.name,
            status: proj.status,
            clientEmail: proj.clientEmail,
            startTime: proj.startTime,
            endTime: proj.endTime,
            description: proj.description,
            percentCompletion: Math.round(proj.percentCompletion,),
            editProject: { val: true },
            deleteProject: { val: true },
            addTask: { val: true },
            showTasks: { val: true },
          }
        })
    }));
  }




}
