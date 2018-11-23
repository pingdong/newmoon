import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpRequest, HttpErrorResponse,
         HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthService } from '@app/shared/auth';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router,
              private authService: AuthService) {}

  public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(request)
        .pipe(
          catchError(response => {
              if (response instanceof HttpErrorResponse && response.status === 401) {
                this.authService.logout('');

                this.router.navigateByUrl('/login');
              }

              const error = response.error.message || response.statusText;
              return throwError(error);
            })
        );

  }

}
