import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard, UnsaveGuard, ValidationGuard } from '../../shared';
import { UserProfileComponent } from './components/user-profile.component';

const routes: Routes = [
  {
    path: '',
    component: UserProfileComponent,
    canActivate: [AuthGuard],
    canDeactivate: [UnsaveGuard, ValidationGuard],
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
export class UserProfileRoutingModule { }
