import { HttpClient } from '@angular/common/http';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { TextToken, Token } from '@angular/compiler/src/ml_parser/tokens';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first, Observable } from 'rxjs';
import { UserService } from '../services/user.service';
import { User } from '../models/user';
import { NgForm } from '@angular/forms';
import { NavbarComponent } from '../navbar/navbar.component';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  navbar!: NavbarComponent
  
  constructor(private http: HttpClient, 
              private userService: UserService,
              private router: Router) { }


  ngOnInit(): void {
  }
  onSubmit(f: NgForm): any {
    this.userService.loginUser(f.value);
    this.userService.isAdministrator();
  }


}
