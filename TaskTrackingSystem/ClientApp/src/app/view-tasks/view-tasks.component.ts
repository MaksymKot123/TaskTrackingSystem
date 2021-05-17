import { Component, OnInit, Input } from '@angular/core';
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

  @Input() projectName: string;
  tasks: ITask[];


  ngOnInit() {
    this.showTasks();
  }

  showTasks() {
    let res = this.taskServ.getTasks(URL, this.projectName).subscribe(x => this.tasks = x);
    console.log(res);
  }

}
