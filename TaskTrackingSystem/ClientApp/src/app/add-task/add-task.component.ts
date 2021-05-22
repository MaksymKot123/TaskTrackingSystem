import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AddNewTaskService } from "src/app/Services/addTaskService";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { TaskDateValidator } from "src/app/DateValidator/taskdatevalidator";

const URL = "https://localhost:44351/tasks/";

@Component({
  selector: 'add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.css']
})
export class AddTaskComponent implements OnInit {

  myForm: FormGroup;
  @Input() projEndTime: string;
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
      "projEndTime": [this.projEndTime, Validators.required],
      "description": [""]
    }, { validators: TaskDateValidator })
  }

  addNewTask() {
    const name = this.myForm.controls["name"].value;
    const description = this.myForm.controls["description"].value;
    const deadline = this.myForm.controls["endTime"].value;
    let deadlineDate = new Date(deadline);

    this.taskServ.addNewTask(URL, this.projName, name, deadlineDate.toISOString(), description).subscribe(null,
      error => { this.error = error });
  }

  get MyForm() { return this.myForm.controls; }

  get EndTime() { return this.myForm.get("endTime"); }

  closeForm(value: { val: boolean }) {
    this.addTask = value;
    this.addTaskChange.emit(value);
    this.isClosedForm = !this.isClosedForm;
  }

}
