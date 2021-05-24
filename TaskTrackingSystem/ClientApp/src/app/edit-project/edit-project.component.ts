import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { FormGroup, FormBuilder, FormControl, Validators } from "@angular/forms";
import { EditProjectService } from "src/app/Services/editProject.service";
import { ProjectDateValidator } from "src/app/DateValidator/projectdatevalidator";

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
  error: any;

  constructor(private http: HttpClient, private projService: EditProjectService) { }

  ngOnInit() {
    let date = new Date(this.endTime);
    let month = date.getMonth() + 1;
    let year = date.getFullYear();
    let day = date.getDate() <= 9 ? "0" + date.getDate() : date.getDate();
    let formatedDate = `${day}/${month}/${year}`;
    this.myForm = new FormGroup({
      "description": new FormControl(this.description, Validators.required),
      "endTime": new FormControl(formatedDate, Validators.required),
      "clientEmail": new FormControl(this.clientEmail, [
        Validators.required,
        Validators.email
      ])
    }, { validators: ProjectDateValidator })
  }

  edit() {
    let wantToEdit = confirm("Do you want to edit this project?");
    if (wantToEdit) {
      const endTime = this.myForm.controls["endTime"].value;
      const description = this.myForm.controls["description"].value;
      const clientEmail = this.myForm.controls["clientEmail"].value;

      const responce = this.projService.editProject(URL,
        this.name, description, clientEmail, new Date(endTime).toISOString()).subscribe(null,
          error => { this.error = error });

      this.ngOnInit();
    }
  }

  get MyForm() { return this.myForm.controls; }

  closeForm(value: { val: boolean }) {
    this.editProject = value;
    this.editProjectChange.emit(value);
    this.isClosedForm = !this.isClosedForm;
  }
}
