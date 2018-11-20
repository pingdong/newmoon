import { Action } from '@ngrx/store';

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

  constructor(public payload: any) { }
}

export class LoginSuccessAction implements Action {
  readonly type = ActionTypes.LOGIN_SUCCESS;

  constructor(public payload: any) { }
}

export class LoginFailureAction implements Action {
  readonly type = ActionTypes.LOGIN_FAILURE;

  constructor(public payload: any) { }
}

export class LogoutAction implements Action {
  readonly type = ActionTypes.LOGOUT;

  constructor(public payload: any) { }
}

export class LogoutSuccessAction implements Action {
  readonly type = ActionTypes.LOGOUT_SUCCESS;
}

export class GetStatusAction implements Action {
  readonly type = ActionTypes.GETSTATUS;
}

export class GetStatusSuccessAction implements Action {
  readonly type = ActionTypes.GETSTATUS_SUCCESS;

  constructor(public payload: any) { }
}

export type All =
  | LoginAction
  | LoginSuccessAction
  | LoginFailureAction
  | LogoutAction
  | LogoutSuccessAction
  | GetStatusAction
  | GetStatusSuccessAction;
