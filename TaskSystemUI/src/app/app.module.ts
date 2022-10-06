import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClient, HttpClientModule, HttpInterceptor, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { TaskComponent } from './task/task.component';
import { TaskApiService } from './services/task-api.service';
import { UserService } from './services/user.service';
import { ShowTaskComponent } from './task/show-task/show-task.component';
import { AddEditTaskComponent } from './task/add-edit-task/add-edit-task.component';
import { LoginComponent } from './login/login.component';
import { NavbarComponent } from './navbar/navbar.component';
import { TokenInterceptorService } from './services/token-interceptor.service';
import { AppRoutingModule, routingComponents } from './app-routing.module';
import { RegisterComponent } from './register/register.component';
import { AdminpanelComponent } from './adminpanel/adminpanel.component';
import { EditUserComponent } from './adminpanel/edit-user/edit-user.component';

@NgModule({
  declarations: [
    AppComponent,
    TaskComponent,
    ShowTaskComponent,
    AddEditTaskComponent,
    LoginComponent,
    NavbarComponent,
    routingComponents,
    RegisterComponent,
    AdminpanelComponent,
    EditUserComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
  ],
  providers: [TaskApiService, UserService, {
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptorService,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
