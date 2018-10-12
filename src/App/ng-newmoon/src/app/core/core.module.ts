import { ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';

import { MaterialModule } from '../material';

import { UnsaveConfirmComponent } from './dirty-check/unsave-confirm/unsave-confirm.component';

@NgModule({
    declarations: [
      UnsaveConfirmComponent
    ],
    imports: [
      MaterialModule,
    ],
    entryComponents: [
      UnsaveConfirmComponent
    ]
  })
  export class CoreModule {

    public static forRoot(): ModuleWithProviders {
      return {
        ngModule: CoreModule,
        providers: [],
      };
    }

    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
      if (parentModule) {
        throw new Error(
          'CoreModule is already loaded. Import it in the AppModule only');
      }
    }

  }
