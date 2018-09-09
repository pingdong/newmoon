import { Component } from '@angular/core';
import { AuthService } from '../../core';

@Component({
  selector: 'app-login',
  styleUrls: ['./login.component.css'],
  templateUrl: './login.component.html',
})
export class LoginComponent {

  constructor(
    public authService: AuthService
  ) { }

  public login(): void {
    this.authService.login('admin', 'passw0rd');
  }

}
