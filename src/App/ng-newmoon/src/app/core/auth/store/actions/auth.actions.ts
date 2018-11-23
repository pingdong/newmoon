import { Action } from '@ngrx/store';

import { LoginPayload } from '../../models/login-payload.model';
import { LoginFailurePayload } from '../../models/login-fail-payload.model';
import { LoginSuccessPayload } from '../../models/login-success-payload.model';
import { LogoutPayload } from '../../models/logout-payload.model';
import { GetStatusSuccessPayload } from '../../models/get-status-success-payload.model';

export enum ActionTypes {
  LOGIN = '[Auth] Login',
  LOGIN_SUCCESS = '[Auth] Login Success',
  LOGIN_FAILURE = '[Auth] Login Faileure',
  LOGOUT = '[Auth] Logout',
  LOGOUT_SUCCESS = '[Auth] Logout Success',
  GETSTATUS = '[Auth] Get Status locally',
  GETSTATUS_SUCCESS = '[Auth] Get Status locally Success',
}

export class LoginAction implements Action {
  readonly type = ActionTypes.LOGIN;

  constructor(public payload: LoginPayload) { }
}

export class LoginSuccessAction implements Action {
  readonly type = ActionTypes.LOGIN_SUCCESS;

  constructor(public payload: LoginSuccessPayload) { }
}

export class LoginFailureAction implements Action {
  readonly type = ActionTypes.LOGIN_FAILURE;

  constructor(public payload: LoginFailurePayload) { }
}

export class LogoutAction implements Action {
  readonly type = ActionTypes.LOGOUT;

  constructor(public payload: LogoutPayload = { username: '' }) { }
}

export class LogoutSuccessAction implements Action {
  readonly type = ActionTypes.LOGOUT_SUCCESS;
}

export class GetStatusAction implements Action {
  readonly type = ActionTypes.GETSTATUS;
}

export class GetStatusSuccessAction implements Action {
  readonly type = ActionTypes.GETSTATUS_SUCCESS;

  constructor(public payload: GetStatusSuccessPayload) { }
}

export type All =
  | LoginAction
  | LoginSuccessAction
  | LoginFailureAction
  | LogoutAction
  | LogoutSuccessAction
  | GetStatusAction
  | GetStatusSuccessAction;
