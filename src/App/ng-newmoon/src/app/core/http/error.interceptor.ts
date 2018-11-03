import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpRequest, HttpErrorResponse,
         HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router) {}

  public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(request)
        .pipe(
          catchError(response => {
              if (response instanceof HttpErrorResponse && response.status === 401) {
                localStorage.removeItem('token');

                this.router.navigateByUrl('/login');
              }

              const error = response.error.message || response.statusText;
              return throwError(error);
            })
        );

  }

}
