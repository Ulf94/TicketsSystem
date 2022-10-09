import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { TaskApiService } from '../services/task-api.service';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {

  taskList$!: Observable<any[]>;

  constructor(private service: TaskApiService) { }

  ngOnInit(): void {
    this.taskList$ = this.service.getTasksList();
  }

}
