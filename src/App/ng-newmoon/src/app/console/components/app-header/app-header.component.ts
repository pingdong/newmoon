import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AppConfig, ConfigService,
         AuthService } from '../../../core';

import {MatSnackBar} from '@angular/material';

@Component({
  selector: 'app-header',
  styleUrls: ['./app-header.component.css'],
  templateUrl: './app-header.component.html',
})
export class AppHeaderComponent implements OnInit {

  public isLoggedIn: boolean;
  public config: AppConfig;
  public username: String;
  public messageCount: number;

  constructor(
    /** @internal */
    private authService: AuthService,
    private configService: ConfigService,
    private router: Router,
    private snackBar: MatSnackBar,
  ) { }

  public ngOnInit(): void {

    this.authService.isLoggedIn$
          .subscribe((result) => {
            this.isLoggedIn = result;
            this.username = 'Ping Dong';
          });

    this.configService.getConfig()
          .subscribe((data) => this.config = {...data} );

    this.messageCount = 8;
  }

  public login(): void {
    this.authService.login('admin', 'passw0rd');
  }

  public logout(): void {
    this.authService.logout();

    this.router.navigate(['/']);
  }

  public gotoMessages(): void {
    this.openSnackBar('Sorry, Message feature is not ready currently.');
  }

  public gotoHelper(): void {
    this.openSnackBar('Sorry, Helper feature is not ready currently.');
  }

  public openAppSettings(): void {
    this.openSnackBar('Sorry, Settings feature is not ready currently.');
  }

  public openUserProfile(): void {
    this.openSnackBar('Sorry, User Profile feature is not ready currently.');
  }

  private openSnackBar(message: string) {
    this.snackBar.open(message, '', {
      duration: 2000,
    });
  }

}
