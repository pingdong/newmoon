import { NgModule } from '@angular/core';

import { UserProfileRoutingModule } from './user-profile.routing';
import { UserProfileComponent } from './components/user-profile.component';

@NgModule({
  declarations: [
    UserProfileComponent,
  ],
  imports: [
    UserProfileRoutingModule,
  ],
})
export class UserProfileModule { }
