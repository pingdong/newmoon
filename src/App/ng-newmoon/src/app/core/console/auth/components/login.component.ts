import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import * as fromStore from '../../../store/app.states';
import { LoginAction, LogoutAction } from '../store/actions/auth.actions';

@Component({
  selector: 'app-login',
  styleUrls: ['./login.component.css'],
  templateUrl: './login.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent implements OnInit, OnDestroy {

  public loginForm = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
  });

  public errorMessage: string;

  private returnUrl: string;
  private destoryed$ = new Subject();

  constructor(
    /** @internal */
    private store: Store<fromStore.AppState>,
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder
  ) { }

  public ngOnInit(): void {
    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

    this.store
        .pipe(
          select('auth'),
          takeUntil(this.destoryed$)
        )
        .subscribe(
          state => {
            if (state && state.errorMessage) {
              this.errorMessage = state.errorMessage;
            }

            if (state && state.token) {
              this.router.navigateByUrl(this.returnUrl);
            }
          }
        );
  }

  public ngOnDestroy(): void {
    this.destoryed$.next();
  }

  public login(): void {
    if (this.loginForm.invalid) {
      return;
    }

    const model = this.loginForm.value;
    const payload = {
      username: model.username,
      password: model.password
    };

    this.store.dispatch(new LoginAction(payload));
  }

  public cancel(): void {
    // Have to send a LogoutAction to reset auth state
    //   Otherwise, a failure login attempt stays in the store
    //   an error message shows up
    this.store.dispatch(new LogoutAction({}));

    this.router.navigateByUrl('/');
  }

}
