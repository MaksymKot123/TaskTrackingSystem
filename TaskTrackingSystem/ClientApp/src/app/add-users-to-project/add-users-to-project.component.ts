import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IProject } from '../Interfaces/iproject';
import { AddUserToProjectService } from "../Services/addUserToProject";
import { GetAllProjectsService } from '../Services/getAllProjects.service';

const URL = "https://localhost:44351/account/addtoproject";
const PROJ_URL = "https://localhost:44351/project/all";

@Component({
  selector: 'add-user-to-project',
  templateUrl: './add-users-to-project.component.html'
})
export class AddUsersToProjectComponent implements OnInit {

  constructor(private service: AddUserToProjectService,
    private projServ: GetAllProjectsService) { }

  ngOnInit() {
    this.getProjects();
  }

  projects: IProject[] = [];
  error: any;
  msg: string;

  @Input() selectedProject: string;
  @Output() selectedProjectChange = new EventEmitter<string>();
  @Input() userEmail: string;

  @Input() addToProject: { val: boolean };
  @Output() addToProjectChange = new EventEmitter<{ val: boolean }>();
  isClosedForm = false;

  getProjects() {
    this.projServ.getAllProjects(PROJ_URL).subscribe(x => this.projects = x,
      error => { this.error = error });
  }

  addUserToProject() {
    if (this.selectedProject != undefined) {
      this.service.addUser(URL, this.selectedProject, this.userEmail).subscribe(() => {
        this.msg = "Project was added to this user";
      },
        error => { this.error = error });
    }
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
