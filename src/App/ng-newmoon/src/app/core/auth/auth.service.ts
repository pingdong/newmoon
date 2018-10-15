import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Injectable({providedIn: 'root'})
export class AuthService {

  public isLoggedIn$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  public forceLogout$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  public redirectUrl: string;

  constructor (
    private router: Router
  ) { }

  public login(username: string, password: string): void {
    this.isLoggedIn$.next(username === 'admin' && password === 'passw0rd');
  }

  public logout(): void {
    this.isLoggedIn$.next(false);

    this.forceLogout$.next(true);
  }

  public redirect(): void {
    if (this.redirectUrl) {
      this.router.navigate([this.redirectUrl]);
    } else {
      this.router.navigate(['/']);
    }
  }

}
