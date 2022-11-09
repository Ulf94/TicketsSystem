import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class TicketApiService {

  //readonly ticketAPIUrl = "https://ticketmanagermaz.azurewebsites.net/api";
  readonly ticketAPIUrl = "https://localhost:44322/api";

  constructor(private http: HttpClient) { }

  getTickets(): Observable<any[]> {
    return this.http.get<any>(this.ticketAPIUrl + '/tickets');
  }

  getTicketsByResponsibleUserID(): Observable<any[]> {
    console.log("get tickets by user id");
    return this.http.get<any>(this.ticketAPIUrl + '/tickets/byresponsibleuser');
  }


  addTicket(data: any) {
    return this.http.post(this.ticketAPIUrl + '/tickets', data);
  }

  updateTicket(id: number | string, data: any) {
    return this.http.patch(this.ticketAPIUrl + `/tickets`, data);
  }

  assignTicket(data: any) {
    return this.http.patch(this.ticketAPIUrl + `/tickets/assignTicket`, data);
  }

  deleteTicket(id: number) {
    return this.http.delete(this.ticketAPIUrl + `/tickets/${id}`);
  }

  // Category

  getCategoryTypesList(): Observable<any[]> {
    return this.http.get<any>(this.ticketAPIUrl + '/categorytypes');
  }

  addCategoryTypes(data: any) {
    return this.http.post(this.ticketAPIUrl + '/categorytypes', data);
  }

  updateCategoryTypes(id: number | string, data: any) {
    return this.http.put(this.ticketAPIUrl + `/categorytypes/${id}`, data);
  }

  deleteCategoryTypes(id: number | string) {
    return this.http.delete(this.ticketAPIUrl + `/categorytypes/${id}`);
  }

  // Status

  getStatusList(): Observable<any[]> {
    return this.http.get<any>(this.ticketAPIUrl + '/statuses');
  }

  addStatus(data: any) {
    return this.http.post(this.ticketAPIUrl + '/statuses', data);
  }

  updateStatus(id: number | string, data: any) {
    return this.http.put(this.ticketAPIUrl + `/statuses/${id}`, data);
  }

  deleteStatus(id: number | string) {
    return this.http.delete(this.ticketAPIUrl + `/statuses/${id}`);
  }
}
