import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TicketApiService {

  //readonly ticketAPIUrl = "https://ticketmanagermaz.azurewebsites.net/api";
  readonly ticketAPIUrl = "https://localhost:44322/api";

  constructor(private http: HttpClient) { }

  getTicketsList(): Observable<any[]> {
    return this.http.get<any>(this.ticketAPIUrl + '/tickets');
  }

  addTicket(data: any) {
    return this.http.post(this.ticketAPIUrl + '/tickets', data);
  }

  updateTicket(id: number | string, data: any) {
    return this.http.put(this.ticketAPIUrl + `/tickets/${id}`, data);
  }

  deleteTicket(id: number | string) {
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
    return this.http.get<any>(this.ticketAPIUrl + '/status');
  }

  addStatus(data: any) {
    return this.http.post(this.ticketAPIUrl + '/status', data);
  }

  updateStatus(id: number | string, data: any) {
    return this.http.put(this.ticketAPIUrl + `/status/${id}`, data);
  }

  deleteStatus(id: number | string) {
    return this.http.delete(this.ticketAPIUrl + `/status/${id}`);
  }
}
