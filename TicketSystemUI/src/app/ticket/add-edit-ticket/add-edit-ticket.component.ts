import { Input, Component, OnInit } from '@angular/core';
import { tick } from '@angular/core/testing';
import { Observable } from 'rxjs';
import { TicketApiService } from 'src/app/services/ticket-api.service';
import { UserService } from 'src/app/services/user.service';


@Component({
  selector: 'app-add-edit-ticket',
  templateUrl: './add-edit-ticket.component.html',
  styleUrls: ['./add-edit-ticket.component.css']
})
export class AddEditTicketComponent implements OnInit {

  ticketList$!: Observable<any[]>;
  statusList$!: Observable<any[]>;
  categoryTypesList$!: Observable<any[]>;

  constructor(private service: TicketApiService
    , private userService: UserService) { }

  @Input() ticket: any;
  id: number = 0;
  ticketName: string = "";
  categoryTypeId: number = 0;
  ticketDescription: string = "";
  statusId: number = 0;
  addedByUserId: number = 0;
  responsibleUserId: number = 0;

  ngOnInit(): void {
    this.id = this.ticket.id;
    this.ticketName = this.ticket.ticketName;
    this.categoryTypeId = this.ticket.categoryTypeId;
    this.ticketDescription = this.ticket.ticketDescription;
    this.statusId = this.ticket.statusId;
    this.addedByUserId = this.ticket.addedByUserId;
    this.responsibleUserId = this.ticket.responsibleUserId;
    this.statusList$ = this.service.getStatusList();
    this.categoryTypesList$ = this.service.getCategoryTypesList();
  }


  addTicket() {
    var ticket = {
      ticketName: this.ticketName,
      ticketDescription: this.ticketDescription,
      categoryTypeId: this.categoryTypeId,
      status: this.statusId,
      addedByUserId: this.userService.getUserId(),
      responsibleUserId: null// todo
    }
    this.service.addTicket(ticket)
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

  updateTicket() {
    var ticket = {
      id: this.id,
      ticketName: this.ticketName,
      ticketDescription: this.ticketDescription,
      categoryTypeId: this.categoryTypeId,
      statusId: this.statusId,
      addedByUserId: this.addedByUserId,
      responsibleUserId: this.responsibleUserId
    }
    console.log(ticket);
    var id: number = this.id;
    this.service.updateTicket(id, ticket).subscribe(result => {
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

  validate() {

  }




}
