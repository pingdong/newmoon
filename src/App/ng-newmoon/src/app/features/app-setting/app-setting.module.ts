import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { SharedModule } from '../../shared';
import { AppSettingRoutingModule } from './app-setting.routing';

import { AppSettingComponent } from './components/app-setting.component';

import { SettingControlService } from './services/setting.control.service';

@NgModule({
  declarations: [
    AppSettingComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,

    AppSettingRoutingModule,
  ],
  providers: [
    // Only used in this modules only
    SettingControlService,
  ]
})
export class AppSettingModule { }
