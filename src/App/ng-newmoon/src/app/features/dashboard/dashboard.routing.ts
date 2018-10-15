import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard, UnsaveGuard, ValidationGuard } from '../../shared';
import { DashboardComponent } from './component/dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
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
export class DashboardRoutingModule { }
