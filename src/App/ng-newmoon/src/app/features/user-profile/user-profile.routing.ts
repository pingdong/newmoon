import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard, UnsaveGuard, ValidationGuard } from '../../shared';
import { UserProfileComponent } from './component/user-profile.component';

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
