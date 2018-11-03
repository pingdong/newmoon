import { Component, Input, OnInit, EventEmitter, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { DyanmicFormTranslateService } from '../../services/dynamic-form.translate.service';
import { DynamicItemBase } from '../../models/dynamic-item.base';
import { UnsaveCheck } from '../../../guards/dirty-check/unsave.check';

@Component({
  selector: 'app-dynamic-form',
  styleUrls: ['./dynamic-form.component.css'],
  templateUrl: './dynamic-form.component.html',
})
export class DynamicFormComponent implements UnsaveCheck, OnInit {

  @Input() items: DynamicItemBase<any>[] = [];

  @Output() save = new EventEmitter<any>();

  private form: FormGroup;

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

}
