import { Component } from '@angular/core';

import { SettingControlService } from '../services/setting.control.service';

@Component({
  selector: 'app-setting',
  templateUrl: './app-setting.component.html',
  styleUrls: ['./app-setting.component.css']
})
export class AppSettingComponent {

  settings: any[];

  constructor(private cs: SettingControlService) {
    this.settings = cs.getDefinition();
  }

  private onSave($event: any) {

  }
}
