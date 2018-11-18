import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as auth from '../auth/store/reducers/auth.reducers';

export interface AppState {
  authState: auth.State;
}

export const reducers = {
  auth: auth.reducer
};

export const authState$ = createFeatureSelector<AppState>('auth');
