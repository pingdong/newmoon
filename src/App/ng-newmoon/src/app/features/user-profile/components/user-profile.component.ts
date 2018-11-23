import { Component, ViewChild, ChangeDetectionStrategy } from '@angular/core';
import { UnsaveCheck } from '@app/core/router';
import { DynamicFormComponent, DynamicItemBase } from '@app/shared/forms';

import { UserProfileControlService } from '../services/user-profile.control.service';

@Component({
  selector: 'app-user-profile',
  styleUrls: ['./user-profile.component.css'],
  templateUrl: './user-profile.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserProfileComponent implements UnsaveCheck {

  public profile: DynamicItemBase<string>[];

  @ViewChild(DynamicFormComponent)  private form: DynamicFormComponent;

  constructor(private cs: UserProfileControlService) {
    this.profile = cs.getDefinition();
  }

  public isDirty(): boolean {
    return this.form.isDirty();
  }

  // tslint:disable-next-line no-any
  private onSave($event: any) {
    this.form.markPristine();
  }

}
