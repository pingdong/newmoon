import { ModuleWithProviders, NgModule } from '@angular/core';

import { MaterialModule } from './material.module';

import { AuthGuard } from './guards/auth/auth.guard';
import { UnsaveGuard } from './guards/dirty-check/unsave.guard';
import { ValidationGuard } from './guards/validation/validation.guard';

import { UnsaveConfirmComponent } from './guards/dirty-check/components/unsave-confirm.component';

import { SelectivePreloadingStrategy } from './routing/selective-preloading-strategy';


@NgModule({
    declarations: [
      UnsaveConfirmComponent,
    ],
    imports: [
      MaterialModule,
    ],
    providers: [
    ],
    exports: [
      MaterialModule,
    ],
    entryComponents: [
      UnsaveConfirmComponent
    ]
  })
  export class SharedModule {

    public static forRoot(): ModuleWithProviders {
      return {
        ngModule: SharedModule,
        providers: [
          AuthGuard,
          UnsaveGuard,
          ValidationGuard,

          SelectivePreloadingStrategy,
        ],
      };
    }

  }
