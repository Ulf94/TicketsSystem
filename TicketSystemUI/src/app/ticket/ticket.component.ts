import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { TicketApiService } from '../services/ticket-api.service';

@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css']
})
export class TicketComponent implements OnInit {

  ticketList$!: Observable<any[]>;

  constructor(private service: TicketApiService) { }

  ngOnInit(): void {
  }

}
