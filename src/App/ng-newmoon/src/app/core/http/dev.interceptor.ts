import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpResponse } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { mergeMap, materialize, delay, dematerialize } from 'rxjs/operators';

@Injectable()
export class DevInterceptor implements HttpInterceptor {

    constructor() {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        return of(null).pipe(
            mergeMap(() => {
                // Login
                if (request.url.endsWith('/login') && request.method === 'POST') {
                    if (request.body.username === 'admin' && request.body.password === 'passw0rd') {
                        const body = {
                            token: 'fake-jwt-token'
                        };

                        return of(new HttpResponse({ status: 200, body: body }));
                    } else {
                        return throwError({ error: { message: 'Username or password is incorrect' } });
                    }
                }

                // Logout
                if (request.url.endsWith('/logout') && request.method === 'POST') {
                    return of (new HttpResponse({ status: 200 }));
                }

                // pass through any requests not handled above
                return next.handle(request);
            }),
            materialize(),
            delay(500),
            dematerialize()
        );
    }

}
