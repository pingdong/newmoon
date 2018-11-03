export class DynamicItemBase<T> {

    key: string;
    value: T;
    label: string;
    required: boolean;
    order: number;
    controlType: string;

    constructor(options: {
        key?: string,
        value?: T,
        label?: string,
        required?: boolean,
        order?: number,
        controlType?: string
      } = {}) {
      this.value = options.value;
      this.key = options.key || '';
      this.label = options.label || '';
      this.required = !!options.required;
      this.order = options.order === undefined ? 1 : options.order;
      this.controlType = options.controlType || '';
    }

}
