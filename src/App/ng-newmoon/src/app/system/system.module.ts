import { CommonModule } from '@angular/common';
import { ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';

import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    CommonModule,
  ],
  providers: [
  ],
})
export class SystemModule {

  public static forRoot(): ModuleWithProviders {
    return {
      ngModule: SystemModule,
      providers: [],
    };
  }

  constructor(@Optional() @SkipSelf() parentModule: SystemModule) {
    if (parentModule) {
      throw new Error(
        'SystemModule is already loaded. Import it in the AppModule only');
    }
  }

}
