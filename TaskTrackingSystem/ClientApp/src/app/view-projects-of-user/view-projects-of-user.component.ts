import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { IProject } from '../Interfaces/iproject';
import { GetProjectsOfUserService } from '../Services/getProjectsOfUser';

const URL = "https://localhost:44351/project";

@Component({
  selector: 'view-projects-of-user',
  templateUrl: './view-projects-of-user.component.html',
  styleUrls: ['./view-projects-of-user.component.css']
})
export class ViewProjectsOfUserComponent implements OnInit {

  constructor(private service: GetProjectsOfUserService) { }

  projects: IProject[] = [];

  @Input() userEmail: string;

  @Input() viewProjects: { val: boolean };
  @Output() viewProjectsChange = new EventEmitter<{ val: boolean }>();
  isClosedForm = false;

  ngOnInit() {
    this.getProject();
  }

  getProject() {
    this.service.getProjects(URL, this.userEmail).subscribe(x => this.projects = x);
  }

  toogle(model: { val: boolean }) {
    model.val = !model.val;
  }

  closeForm(value: { val: boolean }) {
    this.viewProjects = value;
    this.viewProjectsChange.emit(value);
    this.isClosedForm = !this.isClosedForm;
  }
}
