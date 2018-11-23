import { LoginAction, LogoutAction, ActionTypes } from './auth.actions';

describe('LoginAction', () => {
  it('should create an action', () => {
    const payload = {username: 'ping'};
    const action = new LoginAction(payload);

    expect({...action}).toEqual(
      {type: ActionTypes.LOGIN, payload}
    );
  });
});

describe('LoginSuccessAction', () => {
  it('should create an action', () => {});
});

describe('LoginFailureAction', () => {
  it('should create an action', () => {});
});

describe('LogoutAction', () => {
  it('should create an action', () => {
    const action = new LogoutAction();

    expect({...action}).toEqual({type: ActionTypes.LOGIN});
  });
});