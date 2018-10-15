import { CommonModule } from '@angular/common';
import { ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';

import { DashboardComponent } from './dashboard/component/dashboard.component';

@NgModule({
  declarations: [
    DashboardComponent,
  ],
  imports: [
    CommonModule,
  ],
  providers: [
  ],
})
export class FeaturesModule {

  public static forRoot(): ModuleWithProviders {
    return {
      ngModule: FeaturesModule,
      providers: [],
    };
  }

  constructor(@Optional() @SkipSelf() parentModule: FeaturesModule) {
    if (parentModule) {
      throw new Error(
        'SystemModule is already loaded. Import it in the AppModule only');
    }
  }

}
