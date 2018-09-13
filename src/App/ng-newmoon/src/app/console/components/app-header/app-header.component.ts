import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AppConfig, ConfigService,
         AuthService } from '../../../core';

@Component({
  selector: 'app-header',
  styleUrls: ['./app-header.component.css'],
  templateUrl: './app-header.component.html',
})
export class AppHeaderComponent implements OnInit {

  public isLoggedIn: boolean;
  public config: AppConfig;
  public username: String;

  constructor(
    /** @internal */
    private authService: AuthService,
    private configService: ConfigService,
    private router: Router
  ) { }

  public ngOnInit(): void {

    this.authService.isLoggedIn$
          .subscribe((result) => {
            this.isLoggedIn = result;
            this.username = 'Ping Dong';
          });

    this.configService.getConfig()
          .subscribe((data) => this.config = {...data} );
  }

  public login(): void {
    this.authService.login('admin', 'passw0rd');
  }

  public logout(): void {
    this.authService.logout();

    this.router.navigate(['/']);
  }
}
