import { Component, OnInit } from '@angular/core';
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

  projectsReceived = false;
  constructor(private projService: GetAllProjectsService) { }

  ngOnInit() {
    this.getProjects();
    this.projectsReceived = !this.projectsReceived;
  }


  getProjects() {
    this.projService.getAllProjects(URL).subscribe(x => this.projects = x);
  }
}
