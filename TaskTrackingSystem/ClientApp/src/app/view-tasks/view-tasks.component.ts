import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { ITask } from '../Interfaces/itask';
import { EditDeleteTaskService } from '../Services/editDeleteTaskService';
import { GetTasksFromProjectService } from '../Services/getTasksOfProjectService';

const URL = "https://localhost:44351/tasks";

@Component({
  selector: 'view-tasks',
  templateUrl: './view-tasks.component.html',
  styleUrls: ['./view-tasks.component.css']
})
export class ViewTasksComponent implements OnInit {

  constructor(private taskServ: GetTasksFromProjectService,
    private editDeleteTasKServ: EditDeleteTaskService) { }

  readonly Role = localStorage.getItem("role");

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

  delete(taskName: string, projName: string, description: string, status: string,
    startTime: string, endTime: string  ) {
    this.editDeleteTasKServ.deleteTask(URL + "/project/", projName, taskName, description,
      status, startTime, endTime).subscribe();
  }

  edit(taskName: string, projName: string, description: string, status: string,
    startTime: string, endTime: string) {
    this.editDeleteTasKServ.editTask(URL, projName, taskName, description,
      status, startTime, endTime).subscribe();
  }

}
