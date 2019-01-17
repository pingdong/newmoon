import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '@app/shared';

import { DashboardRoutingModule } from './dashboard.routing';
import { DashboardComponent } from './components/dashboard.component';

@NgModule({
  declarations: [
    DashboardComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,

    DashboardRoutingModule,
  ]
})
export class DashboardModule { }
