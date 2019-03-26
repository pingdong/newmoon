import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import {
  DynamicFormItemComponent,
  DynamicFormComponent
} from '@app/shared/forms';

import { MaterialModule } from './material.module';

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
