import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { TaskComponent } from './task/task.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AdminpanelComponent } from './adminpanel/adminpanel.component';


const routes: Routes = [
  { path: '', component: TaskComponent},
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'admin', component: AdminpanelComponent}
];


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes),
    FormsModule
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }

export const routingComponents = [TaskComponent, LoginComponent, RegisterComponent, AdminpanelComponent]
