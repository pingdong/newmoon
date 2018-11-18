import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import * as fromStore from '../../store/app.states';
import { LoginAction, LogoutAction } from '../store/actions/auth.actions';

@Component({
  selector: 'app-login',
  styleUrls: ['./login.component.css'],
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {

  public loginForm = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
  });

  public errorMessage: string;

  private authState$: Observable<any>;
  private returnUrl: string;

  constructor(
    /** @internal */
    private store: Store<fromStore.AppState>,
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.authState$ = this.store.select(fromStore.authState$);
  }

  public ngOnInit(): void {
    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

    this.authState$.subscribe(
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
