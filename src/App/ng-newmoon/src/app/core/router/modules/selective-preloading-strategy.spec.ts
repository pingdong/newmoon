import { async } from '@angular/core/testing';
import { SelectivePreloadingStrategy } from './selective-preloading-strategy';
import { Observable, of } from 'rxjs';

describe('Selective Preloading Strategy', () => {
  const load = function () {
    return of(true);
  };

  it('should log path if preload', () => {
    const strategy = new SelectivePreloadingStrategy();

    const preload$ = <Observable<boolean>>strategy.preload({ data: { preload: true }, path: 'path'}, load);
    preload$.subscribe((preload) => {
      expect(preload).toBe(true);
    });
  });

  it('should not preload if without data property', async(() => {
    const strategy = new SelectivePreloadingStrategy();

    const preload$ = <Observable<boolean>>strategy.preload({path: 'path'}, load);
    preload$.subscribe((preload) => {
      expect(preload).toBeNull();
    });
  }));

  it('should not preload if without preload in data property', () => {
    const strategy = new SelectivePreloadingStrategy();

    const preload$ = <Observable<boolean>>strategy.preload({ data: { load: true }, path: 'path'}, load);
    preload$.subscribe((preload) => {
      expect(preload).toBeNull();
    });
  });

  it('should not preload if with false preload in data property', () => {
    const strategy = new SelectivePreloadingStrategy();

    const preload$ = <Observable<boolean>>strategy.preload({ data: { preload: false }, path: 'path'}, load);
    preload$.subscribe((preload) => {
      expect(preload).toBeNull();
    });
  });

  it('should not preload if without path', () => {
    const strategy = new SelectivePreloadingStrategy();

    const preload$ = <Observable<boolean>>strategy.preload({ data: { preload: true }}, load);
    preload$.subscribe((preload) => {
      expect(preload).toBeNull();
    });
  });

});
