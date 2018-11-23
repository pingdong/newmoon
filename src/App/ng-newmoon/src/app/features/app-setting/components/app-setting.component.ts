import { Component, ViewChild, ChangeDetectionStrategy } from '@angular/core';
import { DynamicFormComponent, DynamicItemBase } from '@app/shared/forms';
import { UnsaveCheck } from '@app/shared/router';

import { SettingControlService } from '../services/setting.control.service';

@Component({
  selector: 'app-setting',
  templateUrl: './app-setting.component.html',
  styleUrls: ['./app-setting.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppSettingComponent implements UnsaveCheck {

  public settings: DynamicItemBase<string>[];

  @ViewChild(DynamicFormComponent)  private form: DynamicFormComponent;

  constructor(private cs: SettingControlService) {
    this.settings = cs.getDefinition();
  }

  public isDirty(): boolean {
    return this.form.isDirty();
  }

  // tslint:disable-next-line no-any
  private onSave($event: any) {
    this.form.markPristine();
  }
}
