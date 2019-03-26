import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UnsaveGuard } from '@app/core/router';

import { UserProfileComponent } from './components/user-profile.component';

const routes: Routes = [
  {
    path: '',
    component: UserProfileComponent,
    canDeactivate: [UnsaveGuard],
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
