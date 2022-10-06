import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../services/user.service';
import { map} from 'rxjs/operators';


export const isLogged = false;

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})

export class NavbarComponent implements OnInit {

  constructor(public userService: UserService,
            private router: Router) { }

  loggedUserName$!: string | null;

  ngOnInit(): void {
    this.userService.currentUser();
  }

  logoutAction():any{
    this.userService.logoutUser();
  }
}
