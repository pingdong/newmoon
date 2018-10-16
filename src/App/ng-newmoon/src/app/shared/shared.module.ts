import { CommonModule } from '@angular/common';
import { ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from './material.module';

import { AuthGuard } from './guards/auth/auth.guard';
import { UnsaveGuard } from './guards/dirty-check/unsave.guard';
import { ValidationGuard } from './guards/validation/validation.guard';

import { UnsaveConfirmComponent } from './guards/dirty-check/component/unsave-confirm.component';

import { SelectivePreloadingStrategy } from './routing/selective-preloading-strategy';


@NgModule({
    declarations: [
      UnsaveConfirmComponent,
    ],
    imports: [
      CommonModule,
      RouterModule,
      FormsModule,
      ReactiveFormsModule,

      MaterialModule,
    ],
    providers: [
      AuthGuard,
      UnsaveGuard,
      ValidationGuard,
    ],
    entryComponents: [
      UnsaveConfirmComponent
    ]
  })
  export class ShareModule {

    public static forRoot(): ModuleWithProviders {
      return {
        ngModule: ShareModule,
        providers: [
          SelectivePreloadingStrategy,
        ],
      };
    }

  }
