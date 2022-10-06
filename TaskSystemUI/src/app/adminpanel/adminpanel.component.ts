import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { TaskApiService } from '../services/task-api.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-adminpanel',
  templateUrl: './adminpanel.component.html',
  styleUrls: ['./adminpanel.component.css']
})
export class AdminpanelComponent implements OnInit {

  constructor(private service: TaskApiService,
    private http: HttpClient,
    public userService: UserService) { }

  usersList$!: Observable<any[]>;
  roleList$!: Observable<any[]>;
  roleList: any = [];
  roleMap: Map<number, string> = new Map();
  modalTitle: string = '';
  activateManageUserComponent: boolean = false;
  user: any;


  ngOnInit(): void {
    this.usersList$ = this.userService.getUsersList();
    this.roleList$ = this.service.getCategoryTypesList();
    this.refreshRoleMap();
  }

  refreshRoleMap() {
    this.userService.getRoleList().subscribe(data => {
      this.roleList = data;

      for (let i = 0; i < data.length; i++) {
        this.roleMap.set(this.roleList[i].id, this.roleList[i].name);
      }
    })
  }

  modalEdit(item: any) {
    this.user = item;
    this.modalTitle = "Manage user";
    this.activateManageUserComponent = true;
  }

  modalClose() {
    this.activateManageUserComponent = false;
    this.usersList$ = this.userService.getUsersList();
  }

  deleteUser(user: any) {
    var confirmationMessage = user.id;
    if (confirm('Are you sure you want to delete user with ID: ' + confirmationMessage)) {
      this.userService.deleteUser(user.id).subscribe(response => {
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
        this.usersList$ = this.userService.getUsersList();
      });
    }
  }

  
}
