import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { FormGroup, FormBuilder, FormControl, Validators } from "@angular/forms";
import { AddNewProjectService } from "src/app/Services/addNewProject.service";
import { ProjectDateValidator } from "src/app/DateValidator/projectdatevalidator";


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
  error: any;
  readonly URL: string;

  constructor(private fb: FormBuilder, private projServ: AddNewProjectService) {
    this.URL = "https:localhost:44351/project";
  }

  ngOnInit() {

    this.myForm = this.fb.group({

      "endTime": ["", Validators.required],
      "projectName": ["", Validators.required],
      "clientEmail": ["", [Validators.required, Validators.email]],
      "description": [""]
    }, { validators: ProjectDateValidator });
  }

  addNewProject() {
    const deadLine = this.myForm.controls["endTime"].value;
    const projectName = this.myForm.controls["projectName"].value;
    const clientEmail = this.myForm.controls["clientEmail"].value;
    const description = this.myForm.controls["description"].value;

    this.projServ.addNewProject("project/", projectName, new Date(deadLine).toISOString(),
      clientEmail, description).subscribe(null, error => { this.error = error });
  }

  get MyForm() { return this.myForm.controls; }

  closeForm(value: boolean) {
    this.addProject = value;
    this.addProjectChange.emit(value);
    this.isClosedForm = !this.isClosedForm;
  }
}
