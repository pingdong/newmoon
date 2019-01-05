import * as auth from './auth.reducers';
import { LoginFailureAction } from '../actions/auth.actions';

describe('Auth Reducers', () => {
  it('should return the default state', () => {
    const { initialState } = auth;
    const action = new LoginFailureAction({ errorMessage: 'error' });
    const afterState = {
        ...initialState,
        isAuthenticated: false,
        username: '',
        token: '',
        errorMessage: 'error'
    };

    const state = auth.reducer(initialState, action);

    expect(state).toEqual(afterState);
  });
});
