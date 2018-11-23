import { Injectable } from '@angular/core';
import { PreloadingStrategy, Route } from '@angular/router';
import { Observable, of } from 'rxjs';

@Injectable({providedIn: 'root'})
export class SelectivePreloadingStrategy implements PreloadingStrategy {

  public preloadedModules: string[] = [];

  // tslint:disable-next-line no-any
  public preload(route: Route, load: () => Observable<any>): Observable<any> {
    if (route.data && route.data['preload'] && route.path) {
        this.preloadedModules.push(route.path);

        return load();
    } else {
        return of(null);
    }
  }

}
