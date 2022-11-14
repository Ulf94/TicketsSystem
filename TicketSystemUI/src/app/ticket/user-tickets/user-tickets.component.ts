import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { TicketApiService } from '../../services/ticket-api.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-tickets',
  templateUrl: './user-tickets.component.html',
  styleUrls: ['./user-tickets.component.css']
})
export class UserTicketsComponent implements OnInit {

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
  ticket: any;
  id: number = 0;
  ticketName: string = "";
  categoryTypeId: number = 0;
  ticketDescription: string = "";
  status: string = "";
  addedByUserId: number = 0;
  currentUserId: string | null = "";
  createdOn: number = 0;
  modalTitle: string = '';
  activateAddEditTicketComponent: boolean = false;


  constructor(private service: TicketApiService,
    private http: HttpClient,
    public userService: UserService) { }

  ngOnInit(): void {
    this.categoryTypesList$ = this.service.getCategoryTypesList();
    this.statusList$ = this.service.getStatusList();
    this.ticketList$ = this.service.getTicketsByResponsibleUserID();
    this.refreshCategoryTypesMap();
    this.refreshStatusMap();
    this.refreshUsersMap();
  }


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
        this.usersMap.set(this.usersList[i].id, (this.usersList[i].firstName + ' ' + this.usersList[i].lastName));
      }
    })
  }




  modalEdit(item: any) {
    this.ticket = item;
    this.modalTitle = "Edit ticket";
    this.activateAddEditTicketComponent = true;
  }
  modalClose() {
    this.activateAddEditTicketComponent = false;
    this.ticketList$ = this.service.getTicketsByResponsibleUserID();
  }
}
