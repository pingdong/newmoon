import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent, PageNotFoundComponent } from './core';
import { SelectivePreloadingStrategy, ShareModule } from './shared';

const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },

  { path: 'dashboard', loadChildren: './features/dashboard/dashboard.module#DashboardModule'},
  //  Preloading
  //  { path: 'setting', loadChildren: './features/app-setting/app-setting.module#AppSettingModule', data: { preload: true }},
  { path: 'setting', loadChildren: './features/app-setting/app-setting.module#AppSettingModule' },
  { path: 'user-profile', loadChildren: './features/user-profile/user-profile.module#UserProfileModule' },

  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  exports: [
    RouterModule,
  ],
  imports: [
    ShareModule,
    RouterModule.forRoot(appRoutes,
      {
        enableTracing: true, // <-- debugging purposes only
        preloadingStrategy: SelectivePreloadingStrategy,
      }),
  ]
})
export class AppRoutingModule { }
