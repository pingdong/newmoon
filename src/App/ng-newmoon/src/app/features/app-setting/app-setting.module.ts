import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { MaterialModule } from '../../shared';

import { AppSettingRoutingModule } from './app-setting.routing';
import { AppSettingComponent } from './component/app-setting.component';

@NgModule({
  declarations: [
    AppSettingComponent,
  ],
  imports: [
    CommonModule,
    MaterialModule,

    AppSettingRoutingModule,
  ]
})
export class AppSettingModule { }
