import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { FormGroup, FormControl, Validators } from "@angular/forms";

@Component({
  selector: 'add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.css']
})
export class AddProjectComponent implements OnInit {

  @Input() addProject: boolean;
  @Output() addProjectChange = new EventEmitter<boolean>();
  isClosedForm = false;

  myForm: FormGroup;

  constructor() { }

  ngOnInit() {
    this.myForm = new FormGroup({
      "projectName": new FormControl("", Validators.required),
      "clientEmail": new FormControl("", [
        Validators.required,
        Validators.email,
      ]),
      "startTime": new FormControl("", Validators.required),
      "entTime": new FormControl("", Validators.required),
      "description": new FormControl("")
    })
  }

  closeForm(value: boolean) {
    this.addProject = value;
    this.addProjectChange.emit(value);
    this.isClosedForm = !this.isClosedForm;
  }

}
