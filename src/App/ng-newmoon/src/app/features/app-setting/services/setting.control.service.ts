import { Injectable } from '@angular/core';

import { DynamicItemBase, SelectionItem, TextItem } from '../../../shared';

@Injectable()
export class SettingControlService {

  // Form definition
  public getDefinition(): DynamicItemBase<any>[] {

    const items: DynamicItemBase<any>[] = [

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
