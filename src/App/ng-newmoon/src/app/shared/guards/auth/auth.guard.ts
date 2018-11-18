import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot,
         CanActivate, CanActivateChild, CanLoad,
         Route, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild, CanLoad {

    constructor(private router: Router,
                private authService: AuthService) {}

    public canLoad(route: Route): boolean {
        const url = `/${route.path}`;

        return this.isLoggedIn(url);
    }

    public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        return this.isLoggedIn(state.url);
    }

    public canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        return this.isLoggedIn(state.url);
    }

    private isLoggedIn(url: string): boolean {
        if (this.authService.getToken()) {
            return true;
        }

        this.router.navigate(['/login'], { queryParams: { returnUrl: url }});

        return false;
    }

}
