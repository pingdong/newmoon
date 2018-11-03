import { Injectable } from '@angular/core';
import { PreloadingStrategy, Route } from '@angular/router';
import { Observable, of } from 'rxjs';

@Injectable()
export class SelectivePreloadingStrategy implements PreloadingStrategy {

  public preloadedModules: string[] = [];

  public preload(route: Route, load: () => Observable<any>): Observable<any> {
    if (route.data && route.data['preload'] && route.path) {
        this.preloadedModules.push(route.path);

        return load();
    } else {
        return of(null);
    }
  }

}
