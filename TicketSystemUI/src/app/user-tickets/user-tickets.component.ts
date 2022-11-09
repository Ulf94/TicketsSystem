import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { TicketApiService } from '../services/ticket-api.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user-tickets',
  templateUrl: './user-tickets.component.html',
  styleUrls: ['./user-tickets.component.css']
})
export class UserTicketsComponent implements OnInit {

  ticketList$!: Observable<any[]>;

  constructor(private service: TicketApiService,
    private http: HttpClient,
    public userService: UserService) { }

  ngOnInit(): void {
    console.log("User ticket ctor");
    this.ticketList$ = this.service.getTicketsByResponsibleUserID();
  }

}
