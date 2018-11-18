import { Component, EventEmitter, Output, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import { ConfigService } from '../../config/config.service';
import { NotificationService } from '../../notification/notification.service';

import * as fromStore from '../../store/app.states';
import { LogoutAction, GetStatusAction } from '../../auth/store/actions/auth.actions';

@Component({
  selector: 'app-header',
  styleUrls: ['./app-header.component.css'],
  templateUrl: './app-header.component.html',
})
export class AppHeaderComponent implements OnInit {

  public isLoggedIn: boolean;
  public title: string;
  public username: String;
  public messageCount: number;

  @Output()
  public sidenavToggled = new EventEmitter();

  private authState$: Observable<any>;

  constructor(
    /** @internal */
    private configService: ConfigService,
    private notificationService: NotificationService,
    private router: Router,
    private store: Store<fromStore.AppState>
  ) {
    this.authState$ = this.store.select(fromStore.authState$);
  }

  // If token is required to be removed after closing browser, browser tab
  //     or navigating to other site.
  // @HostListener('window:beforeunload', ['$event'])
  // handleUnload(event) {
  //   this.logout();
  // }

  public ngOnInit(): void {
    this.configService.getConfig()
          .subscribe((cfg) => this.title = cfg.appTitle );

    this.authState$.subscribe(
      state => {
        if (state && state.token) {
          this.isLoggedIn = state.isAuthenticated;
          this.username = state.username;
        } else {
          this.isLoggedIn = false;
          this.username = '';
        }
      }
    );

    this.store.dispatch(new GetStatusAction());
    this.messageCount = 8;
  }

  public login(): void {
    this.router.navigate(['/login'], { queryParams: { returnUrl: this.router.url }});
  }

  public logout(): void {
    const payload = {
      username: this.username
    };

    this.store.dispatch(new LogoutAction(payload));

    this.router.navigateByUrl('/');
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
