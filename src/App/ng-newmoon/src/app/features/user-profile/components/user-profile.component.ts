import { Component, ViewChild } from '@angular/core';

import { UserProfileControlService } from '../services/user-profile.control.service';
import { DynamicFormComponent, UnsaveCheck } from '../../../shared';

@Component({
  selector: 'app-user-profile',
  styleUrls: ['./user-profile.component.css'],
  templateUrl: './user-profile.component.html',
})
export class UserProfileComponent implements UnsaveCheck {

  public profile: any[];

  @ViewChild(DynamicFormComponent)  private form: DynamicFormComponent;

  constructor(private cs: UserProfileControlService) {
    this.profile = cs.getDefinition();
  }

  public isDirty(): boolean {
    return this.form.isDirty();
  }

  private onSave($event: any) {

  }

}
