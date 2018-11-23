import { Component, ViewChild, ChangeDetectionStrategy } from '@angular/core';
import { UnsaveCheck } from '@app/shared/router';
import { DynamicFormComponent } from '@app/shared/forms';

import { UserProfileControlService } from '../services/user-profile.control.service';

@Component({
  selector: 'app-user-profile',
  styleUrls: ['./user-profile.component.css'],
  templateUrl: './user-profile.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
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
    this.form.markPristine();
  }

}
