import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent, PageNotFoundComponent } from './core';
import { SelectivePreloadingStrategy } from './shared';

const appRoutes: Routes = [

  //  Preloading
  //  { path: 'setting', loadChildren: () => AppSettingModule, data: { preload: true }},
  //  Lazy loading
  //  { path: 'setting', loadChildren: () => AppSettingModule, data: { preload: false }},

  { path: 'login', component: LoginComponent },

  // { path: 'dashboard', component: DashboardComponent },
  // { path: 'setting', loadChildren: () => AppSettingModule, data: { preload: true }},
  // { path: 'user-profile', loadChildren: () => UserProfileModule},

  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  exports: [
    RouterModule,
  ],
  imports: [
    RouterModule.forRoot(appRoutes,
      {
        enableTracing: true, // <-- debugging purposes only
        preloadingStrategy: SelectivePreloadingStrategy,
      }),
  ]
})
export class AppRoutingModule { }
