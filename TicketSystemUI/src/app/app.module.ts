import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClient, HttpClientModule, HttpInterceptor, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { TicketComponent } from './ticket/ticket.component';
import { TicketApiService } from './services/ticket-api.service';
import { UserService } from './services/user.service';
import { ShowTicketComponent } from './ticket/show-ticket/show-ticket.component';
import { AddEditTicketComponent } from './ticket/add-edit-ticket/add-edit-ticket.component';
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
    TicketComponent,
    ShowTicketComponent,
    AddEditTicketComponent,
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
  providers: [TicketApiService, UserService, {
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptorService,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
