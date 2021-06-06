import { Component, OnInit } from '@angular/core';
import { GetAllProjectsService } from "src/app/Services/getAllProjects.service";
import { IProject } from '../Interfaces/iproject';
import { DeleteProjectService } from "src/app/Services/deleteProject.service";

const URL = "https://localhost:44351/project/all";


@Component({
  selector: 'project',
  templateUrl: './project.component.html'
})
export class ProjectComponent implements OnInit {


  projects: IProject[] = [];

  addProject = true;
  showTasks = false;
  error: any;

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
    this.getProjService.getAllProjects(URL).subscribe(x => this.projects = x,
      error => { this.error = error });
  }

  toogle(model: { val: boolean }) {
    model.val = !model.val;
  }

  delete(name: string) {
    let wantToDelete = confirm("Do you want to delete this project?");
    if (wantToDelete) {
      this.delProjService.delete("project", name).subscribe(null,
        error => { this.error = error });
    }
  }
}
