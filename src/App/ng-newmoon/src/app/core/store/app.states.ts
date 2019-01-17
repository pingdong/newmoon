import { createFeatureSelector } from '@ngrx/store';

import * as auth from '@app/core/auth/store/reducers/auth.reducers';

export interface AppState {
  authState: auth.State;
}

export const reducers = {
  auth: auth.reducer
};

export const authState$ = createFeatureSelector<AppState>('auth');
