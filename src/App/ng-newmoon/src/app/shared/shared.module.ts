import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from './material.module';

import { DynamicFormComponent } from './forms/dynamic-forms/components/dynamic-form/dynamic-form.component';
import { DynamicFormItemComponent } from './forms/dynamic-forms/components/dynamic-form-item/dynamic-form-item.component';

@NgModule({
    declarations: [
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
    ]
  })
  export class SharedModule {
  }
