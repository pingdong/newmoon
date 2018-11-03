import { ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from './material.module';

import { AuthGuard } from './guards/auth/auth.guard';
import { UnsaveGuard } from './guards/dirty-check/unsave.guard';

import { UnsaveConfirmComponent } from './guards/dirty-check/components/unsave-confirm.component';
import { DynamicFormComponent } from './dynamic-forms/components/dynamic-form/dynamic-form.component';
import { DynamicFormItemComponent } from './dynamic-forms/components/dynamic-form-item/dynamic-form-item.component';

import { SelectivePreloadingStrategy } from './routing/selective-preloading-strategy';
import { DyanmicFormTranslateService } from './dynamic-forms/services/dynamic-form.translate.service';


@NgModule({
    declarations: [
      UnsaveConfirmComponent,

      DynamicFormComponent,
      DynamicFormItemComponent,
    ],
    imports: [
      CommonModule,
      ReactiveFormsModule,

      MaterialModule,
    ],
    providers: [
      DyanmicFormTranslateService
    ],
    exports: [
      MaterialModule,

      DynamicFormComponent,
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

          SelectivePreloadingStrategy,
        ],
      };
    }

  }
