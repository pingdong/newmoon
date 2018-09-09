import { ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';

import { AuthGuard } from './auth/auth.guard';
import { AuthService } from './auth/auth.service';
import { UnsaveGuard } from './dirty-check/unsave.guard';
import { ValidationGuard } from './validation/validation.guard';
import { ConfigService } from './config/config.service';
import { UserProfileService } from './user-profile/user-profile.service';

@NgModule({
    declarations: [
    ],
    imports: [
    ],
    providers: [
      AuthService,
      ConfigService,

      UserProfileService,

      AuthGuard,
      UnsaveGuard,
      ValidationGuard,
    ],
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
