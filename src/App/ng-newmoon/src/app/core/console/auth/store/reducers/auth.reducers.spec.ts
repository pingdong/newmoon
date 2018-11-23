import * as auth from './auth.reducers';
import { LoginFailureAction } from '../actions/auth.actions';

describe('undefined action', () => {
  it('should return the default state', () => {
    const { initialState } = auth;
    const action = new LoginFailureAction({error: 'error'});
    const afterState = {
        ...initialState,
        errorMessage: 'error'
    };

    const state = auth.reducer(undefined, action);

    expect(state).toEqual(afterState);
  });
});
