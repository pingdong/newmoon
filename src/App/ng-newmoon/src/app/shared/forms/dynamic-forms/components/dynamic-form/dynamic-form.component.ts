import { Component, Input, OnInit, EventEmitter, Output, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
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

  public formGroup: FormGroup;

  constructor(private translate: DyanmicFormTranslateService) {  }

  ngOnInit() {
    this.formGroup = this.translate.toFormGroup(this.items);

    this.markAsPristine();
  }

  public isDirty(): boolean {
    return this.formGroup.dirty;
  }

  public isValid(): boolean {
    return !this.formGroup.invalid;
  }

  public onSubmit(): void {
    this.save.emit(this.formGroup.value);

    this.markAsPristine();
  }

  // tslint:disable-next-line:no-any
  public trackByKey(item: DynamicItemBase<any>) {
    return item.key;
  }

  private markAsPristine(): void {
    Object.keys(this.formGroup.controls).forEach(control => {
      this.formGroup.controls[control].markAsPristine();
    });
  }
}
