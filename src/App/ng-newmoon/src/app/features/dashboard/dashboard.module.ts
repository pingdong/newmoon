import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { MaterialModule } from '../../shared';

import { DashboardRoutingModule } from './dashboard.routing';
import { DashboardComponent } from './component/dashboard.component';

@NgModule({
  declarations: [
    DashboardComponent,
  ],
  imports: [
    CommonModule,

    MaterialModule,
    DashboardRoutingModule,
  ]
})
export class DashboardModule { }
