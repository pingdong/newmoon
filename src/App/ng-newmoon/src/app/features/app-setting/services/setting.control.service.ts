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
        options: [
          {key: 'detial',  value: 'Verbose'},
          {key: 'normal',   value: 'Normal'},
          {key: 'none', value: 'None'}
        ],
        order: 1
      }),

    ];

    return items;

  }

}
