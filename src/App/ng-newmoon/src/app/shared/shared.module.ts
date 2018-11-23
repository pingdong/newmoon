import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from './material.module';

import { UnsaveConfirmComponent } from './router/guards/dirty-check/components/unsave-confirm.component';
import { DynamicFormComponent } from './forms/dynamic-forms/components/dynamic-form/dynamic-form.component';
import { DynamicFormItemComponent } from './forms/dynamic-forms/components/dynamic-form-item/dynamic-form-item.component';

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
  }
