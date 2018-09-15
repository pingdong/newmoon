import { Component } from '@angular/core';
import { UnsaveCheck } from '../../core';

@Component({
  selector: 'app-setting',
  styleUrls: ['./app-setting.component.css'],
  templateUrl: './app-setting.component.html',
})
export class AppSettingComponent implements UnsaveCheck {

  public isDirty(): boolean {
    return true;
  }

}
