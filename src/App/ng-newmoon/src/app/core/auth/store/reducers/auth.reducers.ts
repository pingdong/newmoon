import { All, ActionTypes, LoginSuccessAction,
        LoginFailureAction, LogoutSuccessAction,
        GetStatusSuccessAction } from '../actions/auth.actions';

export interface State {
  isAuthenticated: boolean;
  username: string | null;
  token: string | null;
  errorMessage: string | null;
}

export const initialState: State = {
  isAuthenticated: false,
  username: null,
  token: null,
  errorMessage: null,
};

export function reducer(state = initialState, action: All): State {
  switch (action.type) {

    case ActionTypes.LOGIN_SUCCESS:
      return loginSuccessHandler(state, action);

    case ActionTypes.LOGIN_FAILURE:
      return loginFailureHandler(state, action);

    case ActionTypes.LOGOUT_SUCCESS:
      return logoutSuccessHandler(state, action);

    case ActionTypes.GETSTATUS_SUCCESS:
      return getStatusSuccessHandler(state, action);

  }

  return state;
}

function loginSuccessHandler(state: State, action: LoginSuccessAction) {
  return {
    ...state,
    isAuthenticated: true,
    username: action.payload.username,
    token: action.payload.token,
  };
}

function loginFailureHandler(state: State, action: LoginFailureAction) {
  return {
    ...state,
    errorMessage: action.payload.error
  };
}

function logoutSuccessHandler(state: State, action: LogoutSuccessAction) {
  return {
    ...state,
    isAuthenticated: false,
    username: '',
    token: ''
  };
}

function getStatusSuccessHandler(state: State, action: GetStatusSuccessAction) {
  return {
    ...state,
    ...action.payload
  };
}

