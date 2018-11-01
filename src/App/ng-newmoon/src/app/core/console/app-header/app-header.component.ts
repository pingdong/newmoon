import { Component, EventEmitter, Output, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

import { ConfigService } from '../../config/config.service';
import { AuthService } from '../../auth/services/auth.service';
import { NotificationService } from '../../notification/notification.service';

@Component({
  selector: 'app-header',
  styleUrls: ['./app-header.component.css'],
  templateUrl: './app-header.component.html',
})
export class AppHeaderComponent implements OnInit, OnDestroy {

  public isLoggedIn: boolean;
  public title: string;
  public username: String;
  public messageCount: number;

  @Output()
  public sidenavToggled = new EventEmitter();

  constructor(
    /** @internal */
    private authService: AuthService,
    private configService: ConfigService,
    private notificationService: NotificationService,
    private router: Router,
  ) { }

  public ngOnInit(): void {
    this.authService.isLoggedIn$
          .subscribe((isLoggedIn) => {
            this.isLoggedIn = isLoggedIn;

            if (isLoggedIn) {
              this.username = 'Ping Dong';
            }
          });

    this.configService.getConfig()
          .subscribe((cfg) => this.title = cfg.appTitle );

    this.messageCount = 8;
  }

  public ngOnDestroy(): void {
    this.authService.isLoggedIn$.unsubscribe();
  }

  public login(): void {
    this.router.navigate(['/login'], { queryParams: { returnUrl: this.router.url }});
  }

  public logout(): void {
    this.authService.logout();
  }

  public gotoMessages(): void {
    this.notificationService.sendText('Sorry, Message feature is not ready currently.');
  }

  public gotoHelper(): void {
    this.notificationService.sendText('Sorry, Helper feature is not ready currently.');
  }

  public openAppSettings(): void {
    this.router.navigate(['/setting']);
  }

  public openUserProfile(): void {
    this.router.navigate(['/user-profile']);
  }

  public sidenavClicked(): void {
    this.sidenavToggled.emit();
  }

}
