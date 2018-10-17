import { NgModule } from '@angular/core';

import { UserProfileRoutingModule } from './user-profile.routing';
import { UserProfileComponent } from './component/user-profile.component';

@NgModule({
  declarations: [
    UserProfileComponent,
  ],
  imports: [
    UserProfileRoutingModule,
  ],
})
export class UserProfileModule { }
