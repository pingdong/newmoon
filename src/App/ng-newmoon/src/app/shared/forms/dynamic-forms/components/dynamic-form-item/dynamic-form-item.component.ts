import { Component, Input, ChangeDetectionStrategy } from '@angular/core';
import { FormGroup, AbstractControl } from '@angular/forms';

import { DynamicItemBase } from '../../models/dynamic-item.base';

@Component({
  selector: 'app-dynamic-form-item',
  templateUrl: './dynamic-form-item.component.html',
  styleUrls: ['./dynamic-form-item.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DynamicFormItemComponent {

  @Input() item: DynamicItemBase<any>;
  @Input() parentFormGroup: FormGroup;

  public get isValid() { return this.getControl().valid; }

  public get hasRequiredError(): boolean {
    const error = this.getControl().errors;
    if (error) {
      if (error.required) {
        return error.required;
      }

      return false;
    }

    return false;
  }

  private getControl(): AbstractControl {
    return this.parentFormGroup.controls[this.item.key];
  }
}
