import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent, PageNotFoundComponent, PopupMessageComponent } from './core';
import { SelectivePreloadingStrategy, SharedModule, AuthGuard } from './shared';

const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },

  // Popup, second outlet
  { path: 'message', component: PopupMessageComponent, outlet: 'popup', canActivate: [AuthGuard] },

  // Preloading lazy module
  // { path: 'lazy-module', loadChildren: './features/lazy/lazy.module#LazyModule', data: { preload: true }},
  // If any guard is set to lazy module, event preload is true, the lazy module won't be preloaded
  { path: 'dashboard', loadChildren: './features/dashboard/dashboard.module#DashboardModule'},
  { path: 'setting', loadChildren: './features/app-setting/app-setting.module#AppSettingModule', canLoad: [AuthGuard] },
  { path: 'user-profile', loadChildren: './features/user-profile/user-profile.module#UserProfileModule', canLoad: [AuthGuard] },

  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  exports: [
    RouterModule,
  ],
  imports: [
    SharedModule,

    RouterModule.forRoot(appRoutes,
      {
        enableTracing: true, // <-- debugging purposes only
        preloadingStrategy: SelectivePreloadingStrategy,
      }),
  ]
})
export class AppRoutingModule { }
