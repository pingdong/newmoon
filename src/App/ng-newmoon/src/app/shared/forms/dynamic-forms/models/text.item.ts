import { DynamicItemBase } from './dynamic-item.base';

export class TextItem extends DynamicItemBase<string> {

  controlType = DynamicItemType.Textbox;
  content: string;

  constructor(options: {} = {}) {
    super(options);

    this.content = options['content'] || '';
  }

}
