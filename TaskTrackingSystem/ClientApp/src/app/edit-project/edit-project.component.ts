import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { FormGroup, FormBuilder, FormControl, Validators } from "@angular/forms";
import { EditProjectService } from "src/app/Services/editProject.service";

const URL = "project";


@Component({
  selector: 'edit-project',
  templateUrl: './edit-project.component.html',
  styleUrls: ['./edit-project.component.css']
})
export class EditProjectComponent implements OnInit {

  @Input() editProject: { val: boolean };
  @Output() editProjectChange = new EventEmitter<{ val: boolean }>();
  isClosedForm = false;

  @Input() description: string;
  @Input() endTime: string;
  @Input() clientEmail: string;

  @Input() readonly name: string;
  myForm: FormGroup;

  constructor(private http: HttpClient, private projService: EditProjectService) { }

  ngOnInit() {
    this.myForm = new FormGroup({
      "description": new FormControl(this.description, Validators.required),
      "endTime": new FormControl(this.endTime, Validators.required),
      "clientEmail": new FormControl(this.clientEmail, [
        Validators.required,
        Validators.email
      ])
    })
  }

  edit() {
    const endTime = this.myForm.controls["endTime"].value;
    const description = this.myForm.controls["description"].value;
    const clientEmail = this.myForm.controls["clientEmail"].value;

    const body = {
      "Name": name,
      "EndTime": endTime,
      "Description": description,
      "ClientEmail": clientEmail
    };

    const responce = this.projService.editProject(URL,
      this.name, description, clientEmail, endTime).subscribe();

    this.ngOnInit();
  }

  closeForm(value: { val: boolean }) {
    this.editProject = value;
    this.editProjectChange.emit(value);
    this.isClosedForm = !this.isClosedForm;
  }
}
