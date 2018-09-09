import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class AuthService {

  public isLoggedIn$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  public redirectUrl: string;

  public login(username: string, password: string): void {
    this.isLoggedIn$.next((username === 'admin' && password === 'passw0rd'));
  }

  public logout(): void {
    this.isLoggedIn$.next(false);
  }

}
