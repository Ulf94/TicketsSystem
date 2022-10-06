import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaskApiService {

  //readonly taskAPIUrl = "https://taskmanagermaz.azurewebsites.net/api";
  readonly taskAPIUrl = "https://localhost:44322/api";

  constructor(private http: HttpClient) { }

  getTasksList(): Observable<any[]> {
    return this.http.get<any>(this.taskAPIUrl + '/tasks');
  }

  addTask(data: any) {
    return this.http.post(this.taskAPIUrl + '/tasks', data);
  }

  updateTask(id: number | string, data: any) {
    return this.http.put(this.taskAPIUrl + `/tasks/${id}`, data);
  }

  deleteTask(id: number | string) {
    return this.http.delete(this.taskAPIUrl + `/tasks/${id}`);
  }

  // Category

  getCategoryTypesList(): Observable<any[]> {
    return this.http.get<any>(this.taskAPIUrl + '/categorytypes');
  }

  addCategoryTypes(data: any) {
    return this.http.post(this.taskAPIUrl + '/categorytypes', data);
  }

  updateCategoryTypes(id: number | string, data: any) {
    return this.http.put(this.taskAPIUrl + `/categorytypes/${id}`, data);
  }

  deleteCategoryTypes(id: number | string) {
    return this.http.delete(this.taskAPIUrl + `/categorytypes/${id}`);
  }

  // Status

  getStatusList(): Observable<any[]> {
    return this.http.get<any>(this.taskAPIUrl + '/status');
  }

  addStatus(data: any) {
    return this.http.post(this.taskAPIUrl + '/status', data);
  }

  updateStatus(id: number | string, data: any) {
    return this.http.put(this.taskAPIUrl + `/status/${id}`, data);
  }

  deleteStatus(id: number | string) {
    return this.http.delete(this.taskAPIUrl + `/status/${id}`);
  }
}
