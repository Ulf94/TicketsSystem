import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Form, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { countries } from 'src/environments/environment';
import { User } from '../models/user';
import { UserService } from '../services/user.service';





@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private http: HttpClient, 
    private userService: UserService,
    private router: Router) { }

  userRegisteredSuccessfully : boolean = false;

  public countries:any = countries
  
    ngOnInit(): void {
      this.userRegisteredSuccessfully = false

  }

  

  onSubmit(f: NgForm){
    this.userService.registerUser(f.value).subscribe(
      res =>{
        var showRegisteredSuccess = document.getElementById('register-success-alert');
        if (showRegisteredSuccess) {
          showRegisteredSuccess.style.display = "block";
        }
        setTimeout(function () {
          if (showRegisteredSuccess) {
            showRegisteredSuccess.style.display = "none";
          }
        }, 4000);
      },
      err => {
        //What do do here ? 
      }  
    );
  }

}
