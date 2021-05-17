import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { ITask } from '../Interfaces/itask';
import { GetTasksFromProjectService } from '../Services/getTasksOfProjectService';

const URL = "https://localhost:44351/tasks";

@Component({
  selector: 'view-tasks',
  templateUrl: './view-tasks.component.html',
  styleUrls: ['./view-tasks.component.css']
})
export class ViewTasksComponent implements OnInit {

  constructor(private taskServ: GetTasksFromProjectService) { }

  ngOnInit() {
    this.showTasks();
  }

  @Input() projectName: string;
  @Input() seeTasks: { val: boolean };
  @Output() seeTasksChange = new EventEmitter<{ val: boolean }>();
  isClosedForm = false;

  tasks: ITask[];

  showTasks() {
    this.taskServ.getTasks(URL, this.projectName).subscribe(x => this.tasks = x);
  }

  closeForm(value: { val: boolean }) {
    this.seeTasks = value;
    this.seeTasksChange.emit(value);
    this.isClosedForm = !this.isClosedForm;
  }

}
