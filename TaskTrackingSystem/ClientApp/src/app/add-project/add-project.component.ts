import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { FormGroup, FormBuilder, FormControl, Validators } from "@angular/forms";
import { AddNewProjectService } from "src/app/Services/addNewProject.service";



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
    });
  }

  addNewProject() {
    const deadLine = this.myForm.controls["endTime"].value;
    const projectName = this.myForm.controls["projectName"].value;
    const clientEmail = this.myForm.controls["clientEmail"].value;
    const description = this.myForm.controls["description"].value;

    let responce = this.projServ.addNewProject("project/", projectName, deadLine,
      clientEmail, description).subscribe();
    console.log(responce);
  }

  closeForm(value: boolean) {
    this.addProject = value;
    this.addProjectChange.emit(value);
    this.isClosedForm = !this.isClosedForm;
  }

  validateDate(group: FormGroup) {
    const deadline: string = group.get("entTime").value;
    const deadlineDate = new Date(deadline).getTime();
    const invalid = deadlineDate > Date.now();

    return invalid ? { "invalidDate": true } : null;
  }

  //dateLessThan(from: string, to: string) {
  //  return (group: FormGroup): { [key: string]: any } => {
  //    const f = group.controls[from];
  //    const t = group.controls[to];

  //    if (new Date(f.value) {
  //      return {
  //        dates: "Deadline should be more then current time"
  //      };
  //    }
  //    return {};
  //  }
  //}
}
