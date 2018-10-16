import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { UserProfileRoutingModule } from './user-profile.routing';
import { UserProfileComponent } from './component/user-profile.component';

@NgModule({
  declarations: [
    UserProfileComponent,
  ],
  imports: [
    CommonModule,

    UserProfileRoutingModule,
  ],
})
export class UserProfileModule { }
