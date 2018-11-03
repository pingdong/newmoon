import { Component } from '@angular/core';

import { UserProfileControlService } from '../services/user-profile.control.service';

@Component({
  selector: 'app-user-profile',
  styleUrls: ['./user-profile.component.css'],
  templateUrl: './user-profile.component.html',
})
export class UserProfileComponent {

  profile: any[];

  constructor(private cs: UserProfileControlService) {
    this.profile = cs.getDefinition();
  }

  private onSave($event: any) {

  }

}
