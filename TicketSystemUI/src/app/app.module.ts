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
import { UserTicketsComponent } from './ticket/user-tickets/user-tickets.component';
import { TicketDetailsComponent } from './ticket/ticket-details/ticket-details.component';
import { TicketDetailsUserListComponent } from './ticket/ticket-details-user-list/ticket-details-user-list.component';
import { SendBackComponentComponent } from './send-back-component/send-back-component.component';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthGuard } from './auth.guard';


export function tokenGetter() {
  return localStorage.getItem("token");
}

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
    EditUserComponent,
    UserTicketsComponent,
    TicketDetailsComponent,
    TicketDetailsUserListComponent,
    SendBackComponentComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5001", "localhost:44322"],
        disallowedRoutes: []
      }
    })

  ],
  providers: [TicketApiService,
    UserService, {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true
    },
    AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
