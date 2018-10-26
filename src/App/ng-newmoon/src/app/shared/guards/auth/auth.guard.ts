import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot,
         CanActivate, CanActivateChild, CanLoad,
         Route, Router, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild, CanLoad {

    constructor(private router: Router) {}

    public canLoad(route: Route): boolean {
        const url = `/${route.path}`;

        return this.isLoggedIn(url);
    }

    public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        return this.isLoggedIn(state.url);
    }

    public canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        return this.canActivate(route, state);
    }

    private isLoggedIn(url: string): boolean {
        if (localStorage.getItem('token')) {
            return true;
        }

        this.router.navigate(['/login'], { queryParams: { returnUrl: url }});

        return false;
    }

}
