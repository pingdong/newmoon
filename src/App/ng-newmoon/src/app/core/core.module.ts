import { ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';

import { MaterialModule } from '../material';

import { AuthGuard } from './auth/auth.guard';
import { AuthService } from './auth/auth.service';
import { UnsaveGuard } from './dirty-check/unsave.guard';
import { ValidationGuard } from './validation/validation.guard';
import { ConfigService } from './config/config.service';
import { UserProfileService } from './user-profile/user-profile.service';
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
