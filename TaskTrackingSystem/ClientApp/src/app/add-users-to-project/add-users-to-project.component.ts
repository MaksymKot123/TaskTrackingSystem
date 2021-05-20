import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IProject } from '../Interfaces/iproject';
import { AddUserToProjectService } from "../Services/addUserToProject";
import { GetAllProjectsService } from '../Services/getAllProjects.service';
//import { GetAllProjectsService } from '../Services/getAllProjects.service';
import { GetProjectsOfUserService } from '../Services/getProjectsOfUser';

const URL = "https://localhost:44351/account/addtoproject";
const PROJ_URL = "https://localhost:44351/project/all";

@Component({
  selector: 'add-user-to-project',
  templateUrl: './add-users-to-project.component.html',
  styleUrls: ['./add-users-to-project.component.css']
})
export class AddUsersToProjectComponent implements OnInit {

  constructor(private service: AddUserToProjectService,
    private projServ: GetAllProjectsService
    /*private projServ: GetProjectsOfUserService*/) { }

  ngOnInit() {
    this.getProjects();
  }

  projects: IProject[] = [];
  error: any;

  @Input() selectedProject: string;
  @Output() selectedProjectChange = new EventEmitter<string>();
  @Input() userEmail: string;

  @Input() addToProject: { val: boolean };
  @Output() addToProjectChange = new EventEmitter<{ val: boolean }>();
  isClosedForm = false;

  getProjects() {
    this.projServ.getAllProjects(PROJ_URL).subscribe(x => this.projects = x,
     error => { this.error = error });
    //this.projServ.getProjects(PROJ_URL, this.userEmail).subscribe(x => this.projects = x);
  }

  addUserToProject() {
    this.service.addUser(URL, this.selectedProject, this.userEmail).subscribe(null,
      error => { this.error = error });
  }

  toogle(model: { val: boolean }) {
    model.val = !model.val;
  }

  closeForm(value: { val: boolean }) {
    this.addToProject = value;
    this.addToProjectChange.emit(value);
    this.isClosedForm = !this.isClosedForm;
  }





}
