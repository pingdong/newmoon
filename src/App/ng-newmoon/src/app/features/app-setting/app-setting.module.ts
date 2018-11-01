import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { SharedModule } from '../../shared';

import { AppSettingRoutingModule } from './app-setting.routing';
import { AppSettingComponent } from './components/app-setting.component';

@NgModule({
  declarations: [
    AppSettingComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,

    AppSettingRoutingModule,
  ]
})
export class AppSettingModule { }
