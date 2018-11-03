import { Component, ViewChild } from '@angular/core';

import { SettingControlService } from '../services/setting.control.service';
import { DynamicFormComponent, UnsaveCheck } from '../../../shared';

@Component({
  selector: 'app-setting',
  templateUrl: './app-setting.component.html',
  styleUrls: ['./app-setting.component.css']
})
export class AppSettingComponent implements UnsaveCheck {

  public settings: any[];

  @ViewChild(DynamicFormComponent)  private form: DynamicFormComponent;

  constructor(private cs: SettingControlService) {
    this.settings = cs.getDefinition();
  }

  public isDirty(): boolean {
    return this.form.isDirty();
  }

  private onSave($event: any) {
    this.form.markPristine();
  }
}
