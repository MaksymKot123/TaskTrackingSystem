import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { FormGroup, FormBuilder, FormControl, Validators } from "@angular/forms";

@Component({
  selector: 'edit-project',
  templateUrl: './edit-project.component.html',
  styleUrls: ['./edit-project.component.css']
})
export class EditProjectComponent implements OnInit {

  @Input() editProject: boolean;
  @Output() editProjectChange = new EventEmitter<boolean>();
  isClosedForm = false;

  myForm: FormGroup;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.myForm = new FormGroup({
      "description": new FormControl("", Validators.required),
      "endTime": new FormControl("", Validators.required)
    })
  }

  edit() {
    const endTime = this.myForm.controls["endTime"].value;
    const description = this.myForm.controls["description"].value;


  }

  closeForm(value: boolean) {
    this.editProject = value;
    this.editProjectChange.emit(value);
    this.isClosedForm = !this.isClosedForm;
  }
}
