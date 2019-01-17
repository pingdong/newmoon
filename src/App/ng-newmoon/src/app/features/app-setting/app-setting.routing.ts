import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UnsaveGuard } from '@app/core/router';

import { AppSettingComponent } from './components/app-setting.component';

const routes: Routes = [
  {
    path: '',
    component: AppSettingComponent,
    canDeactivate: [UnsaveGuard]
  },
];

@NgModule({
  exports: [
    RouterModule,
  ],
  imports: [
    RouterModule.forChild(routes),
  ],
})
export class AppSettingRoutingModule { }
