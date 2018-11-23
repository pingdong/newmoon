import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable, of, EMPTY } from 'rxjs';
import { SelectivePreloadingStrategy } from '@app/core/router';

@Injectable({ providedIn: 'root' })
export class DashboardResolverService implements Resolve<number> {

  constructor(
    /** @internal **/
    private preloadStrategy: SelectivePreloadingStrategy,
    private router: Router) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<number> | Observable<never> {

    if (this.preloadStrategy) {
      return of(Date.now());
    } else {
      this.router.navigate(['/dashboard']);

      return EMPTY;
    }
  }

}
