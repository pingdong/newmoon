import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators, AbstractControl } from '@angular/forms';

import { DynamicItemBase } from '../models/dynamic-item.base';

@Injectable({ providedIn: 'root' })
export class DyanmicFormTranslateService {

  // Build Form Group
  public toFormGroup(settings: DynamicItemBase<any>[]): FormGroup {

    if (!settings) {
      throw Error('argument is null');
    }

    const group: { [key: string]: AbstractControl; } = {};
    const sortedItems = settings.sort((a, b) => a.order - b.order);

    sortedItems.forEach(setting => {
      group[setting.key] = setting.required
                                    ? new FormControl(setting.value || '', Validators.required)
                                    : new FormControl(setting.value || '');
    });

    return new FormGroup(group);
  }

}
