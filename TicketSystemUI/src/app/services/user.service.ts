import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, map, Observable } from 'rxjs';
import { isLogged } from '../navbar/navbar.component';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  //readonly taskAPIUrl = "https://taskmanagermaz.azurewebsites.net/api";
  readonly taskAPIUrl = "https://localhost:44322/api";

  isAuthenticated: boolean = false;
  isAdmin: boolean = false;

  loggedUserName: string | null = "";
  loggedRole: string | null = "";

  constructor(private http: HttpClient
    , private router: Router) {
    if (localStorage.getItem("userName") != null) {
      this.loggedUserName = localStorage.getItem("userName");
      this.loggedRole = localStorage.getItem("userRole");
      this.isAuthenticated = true;
      if (localStorage.getItem("userRole") == "Admin") {
        this.isAdmin = true;
        console.log("He's admin")
      }
    }
    else {
      this.loggedUserName = null;
      this.loggedRole = null;
      this.isAuthenticated = false;
      this.isAdmin = false;
    }
  }


  loginUser(userLogin: any) {
    return this.http.post(this.taskAPIUrl + "/UserLogin", userLogin).subscribe((res: any) => {
      this.isAuthenticated = true;
      localStorage.setItem('token', res.token);
      this.currentUser();

      console.log("Logged. Checking role")

      this.router.navigate([''])
    },
      err => {
        console.log(err);
      })

  }

  logoutUser(): any {
    localStorage.removeItem('token');
    localStorage.removeItem('userName');
    localStorage.removeItem('userId');
    localStorage.removeItem('userRole');
    this.loggedUserName = null;
    this.loggedRole = null;
    this.isAuthenticated = false;
    this.isAdmin = false;
    this.router.navigate([''])
  }

  currentUser(): any {
    return this.http.get<any>(this.taskAPIUrl + "/UserLogin/getCurrentUser").subscribe(
      res => {
        localStorage.setItem('userId', res.userId);
        localStorage.setItem('userName', res.userName);
        localStorage.setItem('userRole', res.userRole);
        this.loggedUserName = localStorage.getItem("userName");
        this.loggedRole = localStorage.getItem("userRole");
        this.isAdministrator();
      }
    );
  }

  getToken() {
    return localStorage.getItem("token");
  }

  // Role

  getRoleList(): Observable<any[]> {
    return this.http.get<any>(this.taskAPIUrl + '/roles');
  }

  addRole(data: any) {
    return this.http.post(this.taskAPIUrl + '/roles', data);
  }

  updateRole(id: number | string, data: any) {
    return this.http.put(this.taskAPIUrl + `/roles/${id}`, data);
  }

  deleteRole(id: number | string) {
    return this.http.delete(this.taskAPIUrl + `/roles/${id}`);
  }
  ///

  // Login

  loginTest(): Observable<any[]> {
    return this.http.get<any>(this.taskAPIUrl + '/UserLogin');
  }



  // Users
  getUsersList(): Observable<any[]> {
    return this.http.get<any>(this.taskAPIUrl + '/users');
  }

  updateUser(id: number | string, data: any) {
    return this.http.patch(this.taskAPIUrl + `/users/`, data);
  }


  deleteUser(id: any | string) {
    return this.http.delete(this.taskAPIUrl + `/users/${id}`);
  }

  registerUser(user: any) {
    return this.http.post(this.taskAPIUrl + "/UserRegister", user);
  }

  isLogged(): boolean {
    return this.isAuthenticated;
  }

  isAdministrator(): boolean {
    if (localStorage.getItem("userName") == "Admin") {
      this.isAdmin = true;
      console.log("He's admin")
      return true;

    }
    else {
      this.isAdmin = false;
      return false;
    }


  }

  getUserId(): string | null {
    return localStorage.getItem("userId");
  }

}
