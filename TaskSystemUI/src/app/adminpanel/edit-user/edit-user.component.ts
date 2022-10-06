import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { TaskApiService } from 'src/app/services/task-api.service';
import { UserService } from 'src/app/services/user.service';
import { countries } from 'src/environments/environment';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {

  rolesList$!: Observable<any[]>;
  public countries: any = countries;

  @Input() user: any;
  id: number = 0;
  email: string = "";
  firstName: string = "";
  lastName: string = "";
  dateOfBirth: string = "";
  nationality: string = "";
  roleId: number = 0;
  oldNationality!: Number;
  date: string = "1994-08-24";

  constructor(private service: TaskApiService,
    private http: HttpClient,
    public userService: UserService) { }

  ngOnInit(): void {
    this.id = this.user.id;
    this.email = this.user.email;
    this.firstName = this.user.firstName;
    this.lastName = this.user.lastName;
    this.dateOfBirth = this.user.dateOfBirth.slice(0, 10);
    this.nationality = this.user.nationality;
    this.roleId = this.user.roleId;
    this.rolesList$ = this.userService.getRoleList();
    this.oldNationality = 4;
  }

  manageUser() {
    var user = {
      id: this.id,
      email: this.email,
      firstName: this.firstName,
      lastName: this.lastName,
      roleId: this.roleId,
      dateOfBirth: this.dateOfBirth,
      nationality: this.nationality
    }
    var id: number = this.id;
    this.userService.updateUser(id, user).subscribe(resulet => {
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

}
