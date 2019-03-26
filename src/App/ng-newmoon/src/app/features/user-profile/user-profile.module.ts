import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { SharedModule } from '@app/shared';

import { UserProfileRoutingModule } from './user-profile.routing';
import { UserProfileComponent } from './components/user-profile.component';
import { UserProfileControlService } from './services/user-profile.control.service';

@NgModule({
  declarations: [
    UserProfileComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,

    UserProfileRoutingModule,
  ],
  providers: [
    // Only used in this modules only
    UserProfileControlService,
  ]
})
export class UserProfileModule { }
