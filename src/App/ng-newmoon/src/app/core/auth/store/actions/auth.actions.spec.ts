import { LoginAction, LogoutAction, ActionTypes } from './auth.actions';

describe('Auth Action', () => {

  it('should login', () => {
    const payload = {
      username: 'ping',
      password: 'pwd'
    };
    const action = new LoginAction(payload);

    expect({...action}).toEqual(
      {
        type: ActionTypes.LOGIN,
        payload
      }
    );
  });

  it('should logout', () => {
    const payload = { username: 'ping' };
    const action = new LogoutAction(payload);

    expect({...action}).toEqual(
      {
        type: ActionTypes.LOGOUT,
        payload
      }
    );
  });
});
