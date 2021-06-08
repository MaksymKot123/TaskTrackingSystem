import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { EditProjectService } from "src/app/Services/editProject.service";
import { ProjectDateValidator } from "src/app/DateValidator/projectdatevalidator";

const URL = "project";


@Component({
  selector: 'edit-project',
  templateUrl: './edit-project.component.html'
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
      let endTime:string = this.myForm.controls["endTime"].value;
      const description = this.myForm.controls["description"].value;
      const clientEmail = this.myForm.controls["clientEmail"].value;
      let a: string = "";
      if (endTime[1] == '/' && endTime[3] == '/') {
        a += endTime[2] + '/' + endTime[0] + '/' + endTime.substring(4, endTime.length);
      }
      else if (endTime[2] == '/' && endTime[4] == '/') {
        a += endTime[3] + '/' + endTime.substring(0, 2) + '/' + endTime.substring(5, endTime.length);
      }
      else if (endTime[1] == '/' && endTime[4] == '/') {
        a += endTime.substring(2, 4) + '/' + endTime[0] + '/' + endTime.substring(5, endTime.length);
      }
      else {
        a += endTime.substring(3, 5) + '/' + endTime.substring(0, 2) + '/' + endTime.substring(6, endTime.length);
      }
      console.log(endTime, description, clientEmail);
      console.log(a);
      let date = new Date(a);
      console.log(date);
      this.projService.editProject(URL,
        this.name, description, clientEmail, date.toISOString())
        .subscribe(null, error => { this.error = error });
    }
  }

  get MyForm() { return this.myForm.controls; }

  closeForm(value: { val: boolean }) {
    this.editProject = value;
    this.editProjectChange.emit(value);
    this.isClosedForm = !this.isClosedForm;
  }
}
