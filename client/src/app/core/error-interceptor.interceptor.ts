import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptorInterceptor implements HttpInterceptor {

  ignoreUrls: string[] = [
    '/register'
  ]

  constructor(private router: Router, private toast: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe ( 
      catchError(error => {
        
        if (error && !this.ignoreUrls.find(_ => request.url.endsWith(_))) {
          if (error.status === 400) {
            this.router.navigateByUrl('/not-found');
          }
          else if (error.status === 404) {
            this.toast.error(error.error.message, error.error.status);
          }
          else if (error.status === 500) {
            const extras: NavigationExtras = { state: { error: error.error } };
            this.router.navigateByUrl('/server-error', extras);
          }
        }

        this.toast.error(error.error.message);

        return throwError(error.error.message);
      })); 
  }
}