import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { MaterialModule } from '../../material';

import { AppSettingRoutingModule } from './app-setting-routing.module';
import { AppSettingComponent } from './app-setting.component';

@NgModule({
  declarations: [
    AppSettingComponent,
  ],
  imports: [
    CommonModule,
    MaterialModule,

    AppSettingRoutingModule,
  ],
})
export class AppSettingModule { }
