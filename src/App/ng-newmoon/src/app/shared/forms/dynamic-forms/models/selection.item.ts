import { DynamicItemBase } from './dynamic-item.base';
import { DynamicItemType } from './dynamic-item-type';

export class SelectionItem extends DynamicItemBase<string> {

  controlType = DynamicItemType.Dropdown;
  options: { value: string, text: string }[] = [];

  constructor(options: {} = {}) {
    super(options);
    this.value = options['value'] || '';

    this.options = options['options'] || [];
  }

}
