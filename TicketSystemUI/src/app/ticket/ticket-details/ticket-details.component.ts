import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { TicketApiService } from 'src/app/services/ticket-api.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-ticket-details',
  templateUrl: './ticket-details.component.html',
  styleUrls: ['./ticket-details.component.css']
})
export class TicketDetailsComponent implements OnInit {


  constructor(private service: TicketApiService
    , private userService: UserService) { }

  ticketList$!: Observable<any[]>;
  statusList$!: Observable<any[]>;
  categoryTypesList$!: Observable<any[]>;

  @Input() ticket: any;
  id: number = 0;
  ticketName: string = "";
  categoryTypeId: number = 0;
  ticketDescription: string = "";
  statusId: number = 0;
  addedByUserId: number = 0;
  responsibleUserId: number = 0;
  createdOn: number = 0;

  ngOnInit(): void {
    this.id = this.ticket.id;
    this.ticketName = this.ticket.ticketName;
    this.categoryTypeId = this.ticket.categoryTypeId;
    this.ticketDescription = this.ticket.ticketDescription;
    this.statusId = this.ticket.statusId;
    this.addedByUserId = this.ticket.addedByUserId;
    this.responsibleUserId = this.ticket.responsibleUserId;
    this.createdOn = this.ticket.createdOn;
    this.statusList$ = this.service.getStatusList();
    this.categoryTypesList$ = this.service.getCategoryTypesList();
    console.log(this.ticket);
  }


  // showDetails() {
  //   var ticket = {
  //     id: this.id,
  //     ticketName: this.ticketName,
  //     ticketDescription: this.ticketDescription,
  //     categoryTypeId: this.categoryTypeId,
  //     statusId: this.statusId,
  //     addedByUserId: this.addedByUserId,
  //     responsibleUserId: this.responsibleUserId,
  //     createdOn = this.createdOn,
  //   }
  //   console.log(ticket);
  // }


}
