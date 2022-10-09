import { Injectable } from '@angular/core';
import { HttpClientJsonpModule, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserService } from './user.service';



@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService implements HttpInterceptor {

  constructor(private service: UserService) { }
  intercept(req:any, next:any) {
    let tokenizedReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${this.service.getToken()}`
      }
    })
    return next.handle(tokenizedReq);
  }
}
