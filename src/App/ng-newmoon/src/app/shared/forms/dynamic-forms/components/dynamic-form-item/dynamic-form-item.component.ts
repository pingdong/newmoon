import { Component, Input, ChangeDetectionStrategy } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { DynamicItemBase } from '../../models/dynamic-item.base';

@Component({
  selector: 'app-dynamic-form-item',
  templateUrl: './dynamic-form-item.component.html',
  styleUrls: ['./dynamic-form-item.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DynamicFormItemComponent {

  // tslint:disable-next-line no-any
  @Input() item: DynamicItemBase<any>;
  @Input() form: FormGroup;

  public get isValid() { return this.form.controls[this.item.key].valid; }

}
