import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpResponse } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { mergeMap, materialize, delay, dematerialize } from 'rxjs/operators';

@Injectable()
export class DevInterceptor implements HttpInterceptor {

    private devToken: String = 'fake-jwt-token';

    constructor() {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        return of(null).pipe(
            mergeMap(() => {
                // Login
                if (request.url.endsWith('/login') && request.method === 'POST') {
                    if (request.body.username === 'admin' && request.body.password === 'passw0rd') {
                        const body = {
                            username: 'admin',
                            token: this.devToken
                        };

                        return of(new HttpResponse({ status: 200, body: body }));
                    } else {
                        return throwError({ message: 'Username or password is incorrect' });
                    }
                }

                // Logout
                if (request.url.endsWith('/logout') && request.method === 'POST') {
                    return of (new HttpResponse({ status: 200 }));
                }

                // Verify Token
                if (request.headers.has('token')) {
                    if (request.headers.get('token') !== this.devToken) {
                        return of (new HttpResponse({ status: 401}));
                    }
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
