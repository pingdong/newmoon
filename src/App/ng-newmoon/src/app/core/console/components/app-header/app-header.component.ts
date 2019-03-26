import {
  Component,
  EventEmitter,
  Output,
  OnInit,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  OnDestroy
} from '@angular/core';
import { Router } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { ConfigService, AppConfig } from '@app/core/config';
import { NotificationService } from '@app/core/notification';

import * as fromStore from '@app/core/store/app.states';
import { LogoutAction, GetStatusAction } from '@app/core/auth/store/actions/auth.actions';

@Component({
  selector: 'app-header',
  styleUrls: ['./app-header.component.css'],
  templateUrl: './app-header.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppHeaderComponent implements OnInit, OnDestroy {

  // Using async pipe to detect changes
  public config$: Observable<AppConfig>;

  // Explicitly detect changes by calling markForCheck
  public isLoggedIn: boolean;
  public username: string;
  public messageCount: number;

  @Output()
  public sidenavToggled = new EventEmitter();

  private destoryed$ = new Subject();
  private demoMessageCount = 9;

  constructor(
    /** @internal */
    private configService: ConfigService,
    private notificationService: NotificationService,
    private router: Router,
    private store: Store<fromStore.AppState>,
    private changeDetectorRef: ChangeDetectorRef,
  ) {
  }

  // If token is required to be removed after closing browser, browser tab
  //     or navigating to other site.
  // @HostListener('window:beforeunload', ['$event'])
  // handleUnload(event) {
  //   this.logout();
  // }

  public ngOnInit(): void {

    this.config$ = this.configService.getConfig();

    this.store.pipe(
                select('auth'),
                takeUntil(this.destoryed$)
              )
              .subscribe(
                state => {
                  if (state && state.token) {
                    this.isLoggedIn = state.isAuthenticated;
                    this.username = state.username ? state.username : '';
                  } else {
                    this.isLoggedIn = false;
                    this.username = '';
                  }

                  this.changeDetectorRef.markForCheck();
                }
              );

    this.store.dispatch(new GetStatusAction());
    this.messageCount = this.demoMessageCount;
  }

  public ngOnDestroy(): void {
    this.destoryed$.next();
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

}
