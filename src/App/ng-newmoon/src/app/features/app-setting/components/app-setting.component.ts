import { Component } from '@angular/core';

import { UnsaveCheck } from '../../../shared';

@Component({
  selector: 'app-setting',
  styleUrls: ['./app-setting.component.css'],
  templateUrl: './app-setting.component.html',
})
export class AppSettingComponent implements UnsaveCheck {

  public inSaving = false;

  private dirty = true;

  public isDirty(): boolean {
    return this.dirty;
  }

  public save(): void {
    this.inSaving = true;
    this.dirty = false;

    setTimeout(() => this.inSaving = false, 2000);
  }

}
