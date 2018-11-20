import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from './components/dashboard.component';
import { DashboardResolverService } from './service/dashboard.resolver';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    resolve: {
      resolve: DashboardResolverService
    }
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
export class DashboardRoutingModule { }
