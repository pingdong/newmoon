import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard, UnsaveGuard, ValidationGuard } from '../../core';
import { AppSettingComponent } from './app-setting.component';

const routes: Routes = [
  {
    path: '',
    component: AppSettingComponent,
    canActivate: [AuthGuard],
    canDeactivate: [UnsaveGuard, ValidationGuard]
  },
];

@NgModule({
  exports: [
    RouterModule,
  ],
  imports: [
    RouterModule.forChild(routes),
  ],
})
export class AppSettingRoutingModule { }
