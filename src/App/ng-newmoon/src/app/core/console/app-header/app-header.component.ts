import { Component, EventEmitter, Output, OnInit, ChangeDetectionStrategy, DoCheck, ChangeDetectorRef } from '@angular/core';
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
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppHeaderComponent implements OnInit, DoCheck {

  // Using async pipe to detect changes
  public config$: Observable<any>;

  // Explicitly detect changes by calling markForCheck
  public isLoggedIn: boolean;
  public username: String;
  public messageCount: number;

  @Output()
  public sidenavToggled = new EventEmitter();

  private authState$: Observable<any>;
  private changed = false;

  constructor(
    /** @internal */
    private configService: ConfigService,
    private notificationService: NotificationService,
    private router: Router,
    private store: Store<fromStore.AppState>,
    private changeDetectorRef: ChangeDetectorRef,
  ) {
    this.authState$ = this.store.select(fromStore.authState$);
    this.config$ = this.configService.getConfig();
  }

  // If token is required to be removed after closing browser, browser tab
  //     or navigating to other site.
  // @HostListener('window:beforeunload', ['$event'])
  // handleUnload(event) {
  //   this.logout();
  // }

  public ngOnInit(): void {

    this.authState$.subscribe(
      state => {
        if (state && state.token) {
          this.isLoggedIn = state.isAuthenticated;
          this.username = state.username;
        } else {
          this.isLoggedIn = false;
          this.username = '';
        }

        this.changed = true;
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
    this.router.navigate([{outlets: { popup: 'message' }}]);
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

  ngDoCheck() {
    if (this.changed) {
      this.changeDetectorRef.markForCheck();

      this.changed = false;
    }
  }
}
