import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AddUserToProjectService } from "../Services/addUserToProject";

const URL = "https://localhost:44351/account/addtoproject";

@Component({
  selector: 'add-user-to-project',
  templateUrl: './add-users-to-project.component.html',
  styleUrls: ['./add-users-to-project.component.css']
})
export class AddUsersToProjectComponent implements OnInit {

  constructor(private service: AddUserToProjectService) { }

  ngOnInit() {
  }

  @Input() projectName: string;
  @Input() userEmail: string;

  @Input() addToProject: { val: boolean };
  @Output() addToProjectChange = new EventEmitter<{ val: boolean }>();
  isClosedForm = false;

  addUserToProject() {
    this.service.addUser(URL, this.projectName, this.userEmail).subscribe();
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
