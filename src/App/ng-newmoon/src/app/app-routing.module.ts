import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent, PageNotFoundComponent, PopupMessageComponent } from './core';
import { SelectivePreloadingStrategy, SharedModule, AuthGuard } from './shared';

const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },

  // Popup, second outlet
  { path: 'message', component: PopupMessageComponent, outlet: 'popup', canActivate: [AuthGuard] },

  { path: 'dashboard', loadChildren: './features/dashboard/dashboard.module#DashboardModule'},
  { path: 'setting', loadChildren: './features/app-setting/app-setting.module#AppSettingModule', canLoad: [AuthGuard] },
  { path: 'user-profile',
    loadChildren: './features/user-profile/user-profile.module#UserProfileModule',
    data: { preload: true },
    canLoad: [AuthGuard]
  },

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
