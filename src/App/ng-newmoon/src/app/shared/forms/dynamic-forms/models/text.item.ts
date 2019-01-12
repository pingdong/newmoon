import { DynamicItemBase } from './dynamic-item.base';
import { DynamicItemType } from './dynamic-item-type';

export class TextItem extends DynamicItemBase<string> {

  controlType = DynamicItemType.Textbox;
  inputType: string;

  constructor(options: {} = {}) {
    super(options);
    this.value = options['value'] || '';

    this.inputType = options['inputType'] || 'text';
  }

}
