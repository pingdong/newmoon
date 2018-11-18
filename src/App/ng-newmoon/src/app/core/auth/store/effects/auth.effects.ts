import { Injectable } from '@angular/core';
import { Action } from '@ngrx/store';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { Observable, of } from 'rxjs';
import { map, switchMap, catchError, tap } from 'rxjs/operators';

import { AuthService } from '../../../../shared';
import * as auth from '../actions/auth.actions';

@Injectable()
export class AuthEffects {

  constructor(
    private actions$: Actions,
    private authService: AuthService,
  ) {}

  @Effect()
  login$: Observable<Action> = this.actions$.pipe(
    ofType(auth.ActionTypes.LOGIN),
    map((action: auth.LoginAction) => action.payload),
    switchMap(payload => {
      return this.authService.login(payload.username, payload.password)
        .pipe(
          catchError((error) => {
            return of({ errorMessage: error.message });
          }),
          map((result) => {
            if (result && result.token) {
              return new auth.LoginSuccessAction({ username: result.username, token: result.token });
            } else {
              return new auth.LoginFailureAction({ error: result.errorMessage });
            }
          })
        );
    })
  );

  @Effect({ dispatch: false })
  LogInSuccess$: Observable<any> = this.actions$.pipe(
    ofType(auth.ActionTypes.LOGIN_SUCCESS)
  );

  @Effect({ dispatch: false })
  LoginFailure$: Observable<Action> = this.actions$.pipe(
    ofType(auth.ActionTypes.LOGIN_FAILURE)
  );

  @Effect()
  Logout$: Observable<Action> = this.actions$.pipe(
    ofType(auth.ActionTypes.LOGOUT),
    map((action: auth.LogoutAction) => action.payload),
    tap(payload => {
      this.authService.logout(payload.username);
    })
  );

  @Effect()
  GetStatus$: Observable<Action> = this.actions$.pipe(
    ofType(auth.ActionTypes.GETSTATUS),
    switchMap(_ => {
      return this.authService.loadLocalStatus()
        .pipe(
          catchError(error => {
            return of({ });
          }),
          map((result) => {
            return new auth.GetStatusSuccessAction({ ...result });
          })
        );
    })
  );

  @Effect({ dispatch: false })
  GetStatusSuccess$: Observable<Action> = this.actions$.pipe(
    ofType(auth.ActionTypes.GETSTATUS_SUCCESS)
  );

}
