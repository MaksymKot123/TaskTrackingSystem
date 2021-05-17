import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { GetAllProjectsService } from "src/app/Services/getAllProjects.service";
import { IProject } from '../Interfaces/iproject';
import { DeleteProjectService } from "src/app/Services/deleteProject.service";
import { HttpHeaders } from '@angular/common/http';


const URL = "https://localhost:44351/project/all";
const token = localStorage.getItem("access_token");
const headers = new HttpHeaders({
  'Content-Type': 'application/json',
  'Authorization': `Bearer ${token}`
});


@Component({
  selector: 'project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit {


  projects: IProject[] = [];

  addProject = true;
  showTasks = false;

  constructor(private getProjService: GetAllProjectsService,
    private delProjService: DeleteProjectService) { }

  ngOnInit() {
    this.getProjects();
  }

  viewTasks() {
    this.showTasks = !this.showTasks;
  }

  addNewProject() {
    this.addProject = !this.addProject;
  }


  getProjects() {
    this.getProjService.getAllProjects(URL).subscribe(x => this.projects = x);
  }

  toogle(model: { val: boolean }) {
    model.val = !model.val;
  }

  delete(name: string) {
    let responce = this.delProjService.delete("project", name).subscribe();
    console.log(responce);
  }
}
