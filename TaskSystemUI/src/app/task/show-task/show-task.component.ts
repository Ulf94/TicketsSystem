import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { TaskApiService } from 'src/app/services/task-api.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-show-task',
  templateUrl: './show-task.component.html',
  styleUrls: ['./show-task.component.css']
})
export class ShowTaskComponent implements OnInit {

  taskList$!: Observable<any[]>;
  categoryTypesList$!: Observable<any[]>;
  categoryTypesList: any = [];
  categoryTypesMap: Map<number, string> = new Map();
  usersList$!: Observable<any[]>;
  usersList: any = [];
  usersMap: Map<number, string> = new Map();




  constructor(private service: TaskApiService,
    private http: HttpClient,
    public userService: UserService) { }

  ngOnInit(): void {
    this.taskList$ = this.service.getTasksList();
    this.categoryTypesList$ = this.service.getCategoryTypesList();
    this.usersList$ = this.userService.getUsersList();
    this.refreshCategoryTypesMap();
    this.refreshUsersMap();
  }


  //Variables (properties)
  modalTitle: string = '';
  activateAddEditTaskComponent: boolean = false;
  task: any;

  refreshCategoryTypesMap() {
    this.service.getCategoryTypesList().subscribe(data => {
      this.categoryTypesList = data;

      for (let i = 0; i < data.length; i++) {
        this.categoryTypesMap.set(this.categoryTypesList[i].id, this.categoryTypesList[i].categoryName);
      }
    })
  }

  refreshUsersMap() {
    this.userService.getUsersList().subscribe(res => {
      this.usersList = res;

      for (let i = 0; i < res.length; i++) {
        this.usersMap.set(this.usersList[i].id, this.usersList[i].firstName);
      }
    })
  }

  modalAdd() {
    this.task = {
      id: 0,
      taskName: null,
      categoryTypeId: null,
      taskDescription: null,
      status: null
    }
    this.modalTitle = "Add task";
    this.activateAddEditTaskComponent = true;
  }

  modalEdit(item: any) {
    this.task = item;
    this.modalTitle = "Edit task";
    this.activateAddEditTaskComponent = true;
  }

  modalClose() {
    this.activateAddEditTaskComponent = false;
    this.taskList$ = this.service.getTasksList();
  }

  deleteTask(item: any) {
    var confirmationMessage = item.id;
    if (confirm('Are you sure you want to delete task with ID: ' + confirmationMessage)) {
      this.service.deleteTask(item.id).subscribe(response => {
        var closeModalBtn = document.getElementById('add-edit-modal-close');
        if (closeModalBtn) {
          closeModalBtn.click();
        }

        var showDeleteSuccess = document.getElementById('add-update-alert');
        if (showDeleteSuccess) {
          showDeleteSuccess.style.display = "block";
        }
        setTimeout(function () {
          if (showDeleteSuccess) {
            showDeleteSuccess.style.display = "none";
          }
        }, 4000);
        this.taskList$ = this.service.getTasksList();
      });
    }
  }

}
