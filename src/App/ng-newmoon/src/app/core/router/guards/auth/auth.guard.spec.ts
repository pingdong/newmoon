import { TestBed, async, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';

import { AuthService } from '@app/core/auth';

import { AuthGuard } from './auth.guard';

describe('AuthGuard', () => {

  const authServiceSpy = jasmine.createSpyObj('AuthService', ['getToken']);
  const stateSnapshotSpy = jasmine.createSpy('RouterStateSnapshot');

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        AuthGuard,
        { provide: AuthService, useValue: authServiceSpy },
        { provide: RouterStateSnapshot, useValue: stateSnapshotSpy }
      ],
      imports: [ RouterTestingModule ]
    });
  });

  afterEach(() => {
    authServiceSpy.getToken.calls.reset();
  });

  it('CanLoad() should return true when the user is authenticated',
    // inject your guard service AND Router
    async(inject([AuthGuard, Router], (guard, router) => {
      // Auth Service
      authServiceSpy.getToken.and.returnValue(true);
      // Router spy
      spyOn(router, 'navigate');

      expect(guard.canLoad({path: 'path'})).toBe(true);

      expect(authServiceSpy.getToken.calls.count()).toBe(1);
      expect(router.navigate).not.toHaveBeenCalled();
    })
  ));

  it('CanLoad() should return false when the user is not authenticated',
    // inject your guard service AND Router
    async(inject([AuthGuard, Router], (guard, router) => {
      // Auth Service
      authServiceSpy.getToken.and.returnValue(false);
      // Router spy
      spyOn(router, 'navigate');

      expect(guard.canLoad({path: 'path'})).toBe(false);

      expect(authServiceSpy.getToken.calls.count()).toBe(1);
      expect(router.navigate.calls.count()).toBe(1);
    })
  ));

  it('CanActivate() should return true when the user is authenticated',
    // inject your guard service AND Router
    async(inject([AuthGuard, Router], (guard, router) => {
      // Auth Service
      authServiceSpy.getToken.and.returnValue(true);
      // Router spy
      spyOn(router, 'navigate');

      // Parameters
      const state = router.routerState;
      const snapshot: RouterStateSnapshot = state.snapshot;

      expect(guard.canActivate(new ActivatedRouteSnapshot(), snapshot)).toBe(true);

      expect(authServiceSpy.getToken.calls.count()).toBe(1);
      expect(router.navigate).not.toHaveBeenCalled();
    })
  ));

  it('CanActivate() should return false when the user is not authenticated',
    // inject your guard service AND Router
    async(inject([AuthGuard, Router], (guard, router) => {
      // Auth Service
      authServiceSpy.getToken.and.returnValue(false);
      // Router spy
      spyOn(router, 'navigate');

      // Parameters
      const state = router.routerState;
      const snapshot: RouterStateSnapshot = state.snapshot;

      expect(guard.canActivate(new ActivatedRouteSnapshot(), snapshot)).toBe(false);

      expect(authServiceSpy.getToken.calls.count()).toBe(1);
      expect(router.navigate.calls.count()).toBe(1);
    })
  ));

  it('CanActivateChild() should return true when the user is authenticated',
    // inject your guard service AND Router
    async(inject([AuthGuard, Router], (guard, router) => {
      // Auth Service
      authServiceSpy.getToken.and.returnValue(true);
      // Router spy
      spyOn(router, 'navigate');

      // Parameters
      const state = router.routerState;
      const snapshot: RouterStateSnapshot = state.snapshot;

      expect(guard.canActivateChild(new ActivatedRouteSnapshot(), snapshot)).toBe(true);

      expect(authServiceSpy.getToken.calls.count()).toBe(1);
      expect(router.navigate).not.toHaveBeenCalled();
    })
  ));

  it('CanActivateChild() should return false when the user is not authenticated',
    // inject your guard service AND Router
    async(inject([AuthGuard, Router], (guard, router) => {
      // Auth Service
      authServiceSpy.getToken.and.returnValue(false);
      // Router spy
      spyOn(router, 'navigate');

      // Parameters
      const state = router.routerState;
      const snapshot: RouterStateSnapshot = state.snapshot;

      expect(guard.canActivateChild(new ActivatedRouteSnapshot(), snapshot)).toBe(false);

      expect(authServiceSpy.getToken.calls.count()).toBe(1);
      expect(router.navigate.calls.count()).toBe(1);
    })
  ));

});
