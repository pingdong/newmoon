import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({providedIn: 'root'})
export class AuthService {

  public isLoggedIn$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  private loginUrl = '/login';
  private logoutUrl = '/logout';

  constructor (private http: HttpClient) { }

  public login(username: string, password: string): void {
    this.http.post(this.loginUrl, {username, password})
      .pipe(
        catchError(error => of(`${error}`))
      )
      .subscribe((data: any) => {
        if (data && data.token) {
          localStorage.setItem('token', data.token);

          this.isLoggedIn$.next(true);
        } else {
          this.isLoggedIn$.next(false);
        }
      });
  }

  public logout(): void {
    const req = {
      userid: 0
    };

    this.http.post(this.logoutUrl, req)
      .pipe(
        catchError(error => of(`${error}`))
      )
      .subscribe((data: any) => {
        localStorage.removeItem('token');

        this.isLoggedIn$.next(false);
      });
  }

}
