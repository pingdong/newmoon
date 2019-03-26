import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SharedModule } from '@app/shared';
import {
  PageNotFoundComponent,
  PopupMessageComponent,
  LoginComponent
} from '@app/core';
import {
  SelectivePreloadingStrategy,
  AuthGuard
} from '@app/core/router';

const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },

  // Popup, second outlet
  { path: 'message', component: PopupMessageComponent, outlet: 'popup', canActivate: [AuthGuard] },

  // Preloading lazy module
  // { path: 'lazy-module', loadChildren: './features/lazy/lazy.module#LazyModule', data: { preload: true }},
  // If any guard is set to lazy module, event preload is true, the lazy module won't be preloaded
  { path: 'dashboard', loadChildren: '@app/features/dashboard/dashboard.module#DashboardModule'},
  { path: 'setting', loadChildren: '@app/features/app-setting/app-setting.module#AppSettingModule', canLoad: [AuthGuard] },
  { path: 'user-profile', loadChildren: '@app/features/user-profile/user-profile.module#UserProfileModule', canLoad: [AuthGuard] },

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
