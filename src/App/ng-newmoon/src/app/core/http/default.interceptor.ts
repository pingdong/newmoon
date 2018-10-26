import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

import { AuthService } from '../auth/services/auth.service';

@Injectable()
export class DefaultInterceptor implements HttpInterceptor {

    constructor(public authService: AuthService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        if (!request.url.endsWith('/login')) {
            const token = localStorage.getItem('token');

            if (token) {
                request = request.clone({
                    setHeaders: {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    }
                });
            }
        }

        return next.handle(request);
    }

}
