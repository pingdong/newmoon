import { DynamicItemBase } from './dynamic-item.base';
import { DynamicItemType } from './dynamic-item-type';

export class SelectionItem extends DynamicItemBase<string> {

  controlType = DynamicItemType.Dropdown;
  options: {key: string, value: string}[] = [];

  constructor(options: {} = {}) {
    super(options);

    this.options = options['options'] || [];
  }

}
