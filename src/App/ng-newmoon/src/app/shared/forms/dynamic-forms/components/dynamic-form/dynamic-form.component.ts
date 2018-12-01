import { Component, Input, OnInit, EventEmitter, Output, ChangeDetectionStrategy } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { UnsaveCheck } from '@app/core/router';

import { DyanmicFormTranslateService } from '../../services/dynamic-form.translate.service';
import { DynamicItemBase } from '../../models/dynamic-item.base';

@Component({
  selector: 'app-dynamic-form',
  styleUrls: ['./dynamic-form.component.css'],
  templateUrl: './dynamic-form.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DynamicFormComponent implements UnsaveCheck, OnInit {

  // tslint:disable-next-line no-any
  @Input() items: DynamicItemBase<any>[] = [];

  // tslint:disable-next-line no-any
  @Output() save = new EventEmitter<any>();

  public form: FormGroup;

  constructor(private translate: DyanmicFormTranslateService) {  }

  ngOnInit() {
    this.form = this.translate.toFormGroup(this.items);
  }

  public isDirty(): boolean {
    return this.form.dirty;
  }

  public onSubmit(): void {
    this.save.emit(this.form.value);
  }

  public markPristine(): void {
    Object.keys(this.form.controls).forEach(control => {
      this.form.controls[control].markAsPristine();
    });
  }

  public trackByKey(item: DynamicItemBase<any>) {
    return item.key;
  }
}
