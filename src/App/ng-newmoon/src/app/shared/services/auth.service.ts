import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({providedIn: 'root'})
export class AuthService {

  private loginUrl = '/login';
  private logoutUrl = '/logout';

  constructor (private http: HttpClient) { }

  public login(username: string, password: string): Observable<any> {
    return this.http.post(this.loginUrl, {username, password})
      .pipe(
        tap(state => {
          if (state && state.token) {
            localStorage.setItem('username', state.username);
            localStorage.setItem('token', state.token);
          }
        })
      );
  }

  public logout(username: string): Observable<any> {

    if (username.isNullOrWhitespace()) {
      return of(true);
    }

    const payload = {
      username: username
    };

    return this.http.post(this.logoutUrl, payload)
      .pipe(
        tap(_ => {
          localStorage.removeItem('username');
          localStorage.removeItem('token');
        })
      );
  }

  public getToken(): string | null {
    return localStorage.getItem('token');
  }

  public loadLocalStatus(): Observable<any> {
    const username = localStorage.getItem('username');
    const token = localStorage.getItem('token');

    if (username && token) {
      return of({
        isAuthenticated: true,
        username: username,
        token: token,
      });
    } else {
      localStorage.removeItem('username');
      localStorage.removeItem('token');

      return of({});
    }
  }

}
