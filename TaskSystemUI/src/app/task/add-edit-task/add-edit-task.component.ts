import { Input, Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { TaskApiService } from 'src/app/services/task-api.service';
import { UserService } from 'src/app/services/user.service';


@Component({
  selector: 'app-add-edit-task',
  templateUrl: './add-edit-task.component.html',
  styleUrls: ['./add-edit-task.component.css']
})
export class AddEditTaskComponent implements OnInit {

  taskList$!: Observable<any[]>;
  statusList$!: Observable<any[]>;
  categoryTypesList$!: Observable<any[]>;

  constructor(private service: TaskApiService
            , private userService: UserService) { }

  @Input() task: any;
  id: number = 0;
  taskName: string = "";
  categoryTypeId: number = 0;
  taskDescription: string = "";
  status: string = "";
  addedByUserId: number = 0;

  ngOnInit(): void {
    this.id = this.task.id;
    this.taskName = this.task.taskName;
    this.categoryTypeId = this.task.categoryTypeId;
    this.taskDescription = this.task.taskDescription;
    this.status = this.task.status;
    this.addedByUserId = this.task.addedByUserId;
    this.statusList$ = this.service.getStatusList();
    this.taskList$ = this.service.getTasksList();
    this.categoryTypesList$ = this.service.getCategoryTypesList();
  }


  addTask() {
    var task = {
      taskName: this.taskName,
      taskDescription: this.taskDescription,
      categoryTypeId: this.categoryTypeId,
      status: this.status,
      addedByUserId: this.userService.getUserId(),
      responsibleUserId: null// todo
    }
    this.service.addTask(task)
      .subscribe(
        result => {
          var closeModalBtn = document.getElementById('add-edit-modal-close');
          if (closeModalBtn) {
            closeModalBtn.click();
          }
          var showAddSuccess = document.getElementById('add-success-alert');
          if (showAddSuccess) {
            showAddSuccess.style.display = "block";
          }
          setTimeout(function () {
            if (showAddSuccess) {
              showAddSuccess.style.display = "none";
            }
          }, 4000);
        },
        error => {
          console.log("Not added")
          var closeModalBtn = document.getElementById('add-edit-modal-close');
          if (closeModalBtn) {
            closeModalBtn.click();
          }
          var showAddFailed = document.getElementById('add-failed-alert');
          if (showAddFailed) {
            showAddFailed.style.display = "block";
          }
          setTimeout(function () {
            if (showAddFailed) {
              showAddFailed.style.display = "none";
            }
          }, 4000);
        }
      );

  }

  updateTask() {
    var task = {
      id: this.id,
      taskName: this.taskName,
      taskDescription: this.taskDescription,
      categoryTypeId: this.categoryTypeId,
      status: this.status,
      addedByUserId: this.addedByUserId,
      responsibleUserId: null// todo
    }
    var id: number = this.id;
    this.service.updateTask(id, task).subscribe(resulet => {
      var closeModalBtn = document.getElementById('add-edit-modal-close');
      if (closeModalBtn) {
        closeModalBtn.click();
      }

      var showUpdateSuccess = document.getElementById('add-update-alert');
      if (showUpdateSuccess) {
        showUpdateSuccess.style.display = "block";
      }
      setTimeout(function () {
        if (showUpdateSuccess) {
          showUpdateSuccess.style.display = "none";
        }
      }, 4000);

    })
  }

  validate(){
    
  }




}
