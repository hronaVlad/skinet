import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toast: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe ( 
      catchError(error => {
        if (error) {
          if (error.status === 400) {
            this.router.navigateByUrl('/not-found');
          }
          else if (error.status === 404) {
            this.toast.error(error.error.message, error.error.status);
          }
          else if (error.status === 500) {
            this.router.navigateByUrl('/server-error');
          }
        }
        return throwError(error);
      })); 
  }
}