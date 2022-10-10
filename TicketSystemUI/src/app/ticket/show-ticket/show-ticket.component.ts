import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { TicketApiService } from 'src/app/services/ticket-api.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-show-ticket',
  templateUrl: './show-ticket.component.html',
  styleUrls: ['./show-ticket.component.css']
})
export class ShowTicketComponent implements OnInit {

  ticketList$!: Observable<any[]>;
  categoryTypesList$!: Observable<any[]>;
  categoryTypesList: any = [];
  categoryTypesMap: Map<number, string> = new Map();
  statusMap: Map<number, string> = new Map();
  statusList: any = [];
  statusList$!: Observable<any[]>;
  usersList$!: Observable<any[]>;
  usersList: any = [];
  usersMap: Map<number, string> = new Map();




  constructor(private service: TicketApiService,
    private http: HttpClient,
    public userService: UserService) { }

  ngOnInit(): void {
    this.ticketList$ = this.service.getTicketsList();
    this.categoryTypesList$ = this.service.getCategoryTypesList();
    this.statusList$ = this.service.getStatusList();
    this.usersList$ = this.userService.getUsersList();
    this.refreshCategoryTypesMap();
    this.refreshStatusMap();
    this.refreshUsersMap();
  }


  //Variables (properties)
  modalTitle: string = '';
  activateAddEditTicketComponent: boolean = false;
  ticket: any;

  refreshCategoryTypesMap() {
    this.service.getCategoryTypesList().subscribe(data => {
      this.categoryTypesList = data;

      for (let i = 0; i < data.length; i++) {
        this.categoryTypesMap.set(this.categoryTypesList[i].id, this.categoryTypesList[i].categoryName);
      }
    })
  }

  refreshStatusMap() {
    this.service.getStatusList().subscribe(data => {
      this.statusList = data;

      for (let i = 0; i < data.length; i++) {
        this.statusMap.set(this.statusList[i].id, this.statusList[i].statusOption);
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
    this.ticket = {
      id: 0,
      ticketName: null,
      categoryTypeId: null,
      ticketDescription: null,
      status: null
    }
    this.modalTitle = "Add ticket";
    this.activateAddEditTicketComponent = true;
  }

  modalEdit(item: any) {
    this.ticket = item;
    this.modalTitle = "Edit ticket";
    this.activateAddEditTicketComponent = true;
  }

  modalClose() {
    this.activateAddEditTicketComponent = false;
    this.ticketList$ = this.service.getTicketsList();
  }

  deleteTicket(item: any) {
    var confirmationMessage = item.id;
    if (confirm('Are you sure you want to delete ticket with ID: ' + confirmationMessage)) {
      this.service.deleteTicket(item.id).subscribe(response => {
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
        this.ticketList$ = this.service.getTicketsList();
      });
    }
  }

}
