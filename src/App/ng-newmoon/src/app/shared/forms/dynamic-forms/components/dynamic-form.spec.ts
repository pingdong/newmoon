import { async, ComponentFixture, TestBed, fakeAsync } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';

import { SelectionItem, TextItem, DynamicFormComponent } from '../..';
import { DynamicFormItemComponent } from './dynamic-form-item/dynamic-form-item.component';
import { DyanmicFormTranslateService } from '../services/dynamic-form.translate.service';

describe('DynamicForm', () => {

  let component: DynamicFormComponent;
  let fixture: ComponentFixture<DynamicFormComponent>;

  const dropdownOptions = [
    { value: 'detail', text: 'Verbose' },
    { value: 'normal', text: 'Normal' },
    { value: 'none', text: 'None' }
  ];

  const expectedControls = [

    new SelectionItem({
      key: 'logLevel',
      label: 'Logging Level',
      options: dropdownOptions,
      value: 'detail',
      order: 1
    }),

    new TextItem ({
      key: 'productName',
      label: 'Product Name',
      value: 'InitialValue',
      required: true
    }),

  ];

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        DynamicFormComponent,
        DynamicFormItemComponent
      ],
      imports: [
        ReactiveFormsModule
      ],
      providers: [
        DyanmicFormTranslateService
      ]
    })
    .compileComponents();
  }));

  beforeEach(async(() => {
    fixture = TestBed.createComponent(DynamicFormComponent);
    component = fixture.componentInstance;

    // simulate the parent setting the input property with that hero
    component.items = expectedControls;

    fixture.detectChanges();
  }));

  it('should create properly', fakeAsync(() => {

    // Forms
    //  IsDirty = false
    expect(component.isDirty()).toBe(false, 'IsDirty should be false');
    expect(component.formGroup.dirty).toBe(false, 'dirty should be false');
    //  IsValid = true
    expect(component.isValid()).toBe(true, 'isValid should be true');
    expect(component.formGroup.invalid).toBe(false, 'form should be valid');
    expect(component.formGroup.status).toEqual('VALID', 'status should be VALID');
    //  Error Message
    const errors = fixture.debugElement.queryAll(By.css('.form__error-message'));
    // tslint:disable-next-line:no-magic-numbers
    expect(errors.length).toEqual(0, 'Should not have any error');

    // Common
    //  Label for every item
    const labels = fixture.debugElement.queryAll(By.css('.form__label'));
    expect(labels.length).toEqual(expectedControls.length, 'The number of controls doesn\'t match its expectation');
    for (let i = 0; i < expectedControls.length; i++) {
      expect(labels[i].nativeElement.innerText).toEqual(expectedControls[i].label, 'Label doesn\'t match its setting');
      expect(labels[i].nativeElement.getAttribute('for')).toEqual(expectedControls[i].key, 'Label doesn\'t match its setting');
    }

    // Controls
    //  Textbox
    const textbox = fixture.nativeElement.querySelector('input');
    expect(textbox.getAttribute('type')).toEqual('text', 'Expect a textbox here');
    expect(textbox.getAttribute('id')).toEqual(expectedControls[1].key, 'Id doesn\'t match the expect id');
    //    Initial value
    expect(textbox.value).toEqual(expectedControls[1].value, 'Initial value doesn\'t match setting');
    //  Dropdown
    const dropdown = fixture.nativeElement.querySelector('select');
    expect(dropdown.getAttribute('id')).toEqual(expectedControls[0].key, 'Id doesn\'t match the expect id');
    //    Drop down options
    expect(dropdown.options.length).toEqual(dropdownOptions.length, 'The number of the candidate value doesn\'t match setting');
    for (let i = 0; i < dropdownOptions.length; i++) {
      expect(dropdown.options[i].value).toEqual(dropdownOptions[i].value, 'The option value doesn\'t match setting');
    }
    //    Initial value
    expect(dropdown.value).toEqual(expectedControls[0].value, 'Initial value doesn\'t match setting');

  }));

  it('update value properly', fakeAsync(() => {

    const testString = 'testValue';
    const testSelection = 'none';

    // Update value
    //  Textbox
    component.formGroup.controls.productName.setValue(testString);
    //  Dropdown
    component.formGroup.controls.logLevel.setValue(testSelection);
    //  Trigger event
    const textbox = fixture.nativeElement.querySelector('input');
    textbox.dispatchEvent(new Event('input'));
    fixture.detectChanges();

    // Textbox
    expect(component.formGroup.controls.productName.value).toEqual(testString, 'The value of control doesn\'t match the provided value');
    // Dropdown
    expect(component.formGroup.controls.logLevel.value).toEqual(testSelection, 'The value of control doesn\'t match the provided value');

    // Value
    expect(component.formGroup.value).toEqual({ logLevel: testSelection, productName: testString });
    // IsDirty = True
    expect(component.isDirty()).toBe(true, 'IsDirty should be true after updated');
    expect(component.formGroup.dirty).toBe(true, 'Dirty should be true after updated');
    // IsValid = True
    expect(component.isValid()).toBe(true, 'IsValid should be true after updated');
    expect(component.formGroup.status).toEqual('VALID', 'Status should be VALID after updated');
    // Error Message
    const errors = fixture.debugElement.queryAll(By.css('.form__error-message'));
    expect(errors.length).toEqual(0, 'Should not have any error message');

  }));

  it('should raise error if required value not provided', fakeAsync(() => {

    // Textbox
    component.formGroup.controls.productName.setValue('');
    //  Trigger event
    const textbox = fixture.nativeElement.querySelector('input');
    textbox.dispatchEvent(new Event('input'));
    fixture.detectChanges();

    // Evaluate
    expect(component.formGroup.controls.productName.value).toEqual('', 'Value should be cleared');
    expect(component.formGroup.controls.productName.errors).toEqual({ required: true }, 'Should have required error');
    //  IsDirty = True
    expect(component.isDirty()).toBe(true, 'IsDirty should be true after updated');
    expect(component.formGroup.dirty).toBe(true, 'Dirty should be true after updated');
    //  IsValid = True
    expect(component.isValid()).toBe(false, 'IsValid should be false after updated');
    expect(component.formGroup.status).toEqual('INVALID', 'Status should be INVALID after updated');
    //  Error Message
    const errors = fixture.debugElement.queryAll(By.css('.form__error-message'));
    expect(errors.length).toEqual(1, 'Should have an error message');

    // Save button
    const btnSave = fixture.nativeElement.querySelector('button');
    expect(btnSave.disabled).toBe(true, 'Can\'t save when value is invalid');

  }));

  it('should trigger event after clicked "Save"', fakeAsync(() => {

    // Setup
    let eventTriggered = false;

    component.save.subscribe((data) => {
      if (!data) {
        eventTriggered = false;
      }

      eventTriggered = data.productName === 'new value' && data.logLevel === expectedControls[0].value;
    });

    // Update data
    component.formGroup.controls.productName.setValue('new value');
    const textbox = fixture.nativeElement.querySelector('input');
    textbox.dispatchEvent(new Event('input'));

    // Click Save
    fixture.debugElement.query(By.css('form')).triggerEventHandler('ngSubmit', null);
    fixture.detectChanges();

    // Check Save is triggered
    expect(eventTriggered).toBe(true, 'Save event should be triggered');

    // IsDirty = True
    expect(component.isDirty()).toBe(false, 'After saving, isDirty should be false');
    expect(component.formGroup.dirty).toBe(false, 'After saving, dirty should be false');

  }));

});
