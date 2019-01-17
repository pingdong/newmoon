import { Injectable } from '@angular/core';

import { DynamicItemBase, SelectionItem } from '@app/shared/forms';

@Injectable()
export class SettingControlService {

  // Form definition
  public getDefinition(): DynamicItemBase<string>[] {

    const items: DynamicItemBase<string>[] = [

      new SelectionItem({
        key: 'logLevel',
        label: 'Logging Level',
        value: 'none',
        options: [
          { value: 'detail',  text: 'Verbose' },
          { value: 'normal',   text: 'Normal' },
          { value: 'none', text: 'None' }
        ],
        order: 1
      }),

    ];

    return items;

  }

}
