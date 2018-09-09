import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SelectivePreloadingStrategy } from './core';
import { LoginComponent, PageNotFoundComponent } from './share';
import { AppSettingModule, DashboardComponent, UserProfileModule } from './system';

import { AddressService } from './address.service';

const appRoutes: Routes = [

  //  Preloading
  //  { path: 'setting', loadChildren: () => AppSettingModule, data: { preload: true }},
  //  Lazy loading
  //  { path: 'setting', loadChildren: () => AppSettingModule, data: { preload: false }},

  { path: 'login', component: LoginComponent },

  { path: 'dashboard', component: DashboardComponent },
  { path: 'setting', loadChildren: () => AppSettingModule, data: { preload: true }},
  { path: 'user-profile', loadChildren: () => UserProfileModule},

  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
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
  ],
  providers: [
    SelectivePreloadingStrategy,
    AddressService,
  ],
})
export class AppRoutingModule { }
