import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { TicketApiService } from 'src/app/services/ticket-api.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-ticket-details-user-list',
  templateUrl: './ticket-details-user-list.component.html',
  styleUrls: ['./ticket-details-user-list.component.css']
})
export class TicketDetailsUserListComponent implements OnInit {


  constructor(private service: TicketApiService
    , private userService: UserService) { }

  ticketList$!: Observable<any[]>;
  statusList$!: Observable<any[]>;
  categoryTypesList$!: Observable<any[]>;
  categoryTypesMap: Map<number, string> = new Map();
  categoryTypesList: any = [];

  @Input() ticket: any;
  id: number = 0;
  ticketName: string = "";
  categoryTypeId: number = 0;
  categoryName: string = "";
  ticketDescription: string = "";
  status: string = "";
  addedByUser: string = "";
  responsibleUser: string = "";
  createdOn: number = 0;

  ngOnInit(): void {
    this.id = this.ticket.id;
    this.ticketName = this.ticket.ticketName;
    this.categoryName = this.ticket.categoryName;
    this.ticketDescription = this.ticket.ticketDescription;
    this.status = this.ticket.status;
    this.addedByUser = this.ticket.addedByUser;
    this.responsibleUser = this.ticket.responsibleUser;
    this.createdOn = this.ticket.createdOn;
    this.statusList$ = this.service.getStatusList();
    this.categoryTypesList = this.service.getCategoryTypesList();
    console.log("Ticket received in detailed view: " + this.ticket.id);;
  }
}
