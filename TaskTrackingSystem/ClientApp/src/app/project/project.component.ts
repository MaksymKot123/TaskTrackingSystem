import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { GetAllProjectsService } from "src/app/Services/getAllProjects.service";
import { IProject } from '../Interfaces/iproject';


const URL = "https://localhost:44351/project/all";

@Component({
  selector: 'project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit {


  projects: IProject[] = [];

  addProject = true;

  constructor(private projService: GetAllProjectsService) { }

  ngOnInit() {
    this.getProjects();
  }

  addNewProject() {
    this.addProject = !this.addProject;
  }


  getProjects() {
    this.projService.getAllProjects(URL).subscribe(x => this.projects = x);
  }
}
