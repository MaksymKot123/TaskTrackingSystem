import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AddNewTaskService } from "src/app/Services/addTaskService";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

const URL = "https://localhost:44351/tasks/";

@Component({
  selector: 'add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.css']
})
export class AddTaskComponent implements OnInit {

  myForm: FormGroup;
  @Input() projName: string;
  @Input() addTask: { val: boolean };
  @Output() addTaskChange = new EventEmitter<{ val: boolean }>();
  isClosedForm = false;

  error: any;

  constructor(private taskServ: AddNewTaskService, private fb: FormBuilder) { }

  ngOnInit() {
    this.myForm = this.fb.group({
      "name": ["", Validators.required],
      "endTime": ["", Validators.required],
      "description": [""]
    });
  }

  addNewTask() {
    const name = this.myForm.controls["name"].value;
    const description = this.myForm.controls["description"].value;
    const deadline = this.myForm.controls["endTime"].value;

    this.taskServ.addNewTask(URL, this.projName, name, deadline, description).subscribe(null,
      error => { this.error = error });

    this.ngOnInit();
  }

  closeForm(value: { val: boolean }) {
    this.addTask = value;
    this.addTaskChange.emit(value);
    this.isClosedForm = !this.isClosedForm;
  }

}
