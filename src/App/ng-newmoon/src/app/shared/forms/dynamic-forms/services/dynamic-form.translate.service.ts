import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators, AbstractControl } from '@angular/forms';

import { DynamicItemBase } from '../models/dynamic-item.base';

@Injectable({providedIn: 'root'})
export class DyanmicFormTranslateService {

  // Build Form Group
  // tslint:disable-next-line no-any
  public toFormGroup(settings: DynamicItemBase<any>[] ) {

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
