export class DynamicItemBase<T> {

    key: string;
    value?: T;
    label: string;
    required: boolean;
    order: number;
    controlType: DynamicItemType;

    constructor(option: {
        key?: string,
        value?: T,
        label?: string,
        required?: boolean,
        order?: number,
        controlType?: DynamicItemType,
      } = { }) {
      this.value = option.value;
      this.key = option.key || '';
      this.label = option.label || '';
      this.required = !!option.required;
      this.order = option.order === undefined ? 1 : option.order;
      this.controlType = option.controlType || DynamicItemType.Textbox;
    }

}
