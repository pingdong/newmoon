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

  beforeEach(() => {
    fixture = TestBed.createComponent(DynamicFormComponent);
    component = fixture.componentInstance;
    // simulate the parent setting the input property with that hero
    component.items = expectedControls;

    // trigger initial data binding
    fixture.detectChanges();
  });

  it('should create properly', fakeAsync(() => {

    // Forms
    //  IsDirty = false
    expect(component.isDirty()).toBe(false);
    expect(component.formGroup.dirty).toBe(false);
    //  IsValid = true
    expect(component.isValid()).toBe(true);
    expect(component.formGroup.invalid).toBe(false);
    expect(component.formGroup.status).toEqual('VALID');
    //  Error Message
    const errors = fixture.debugElement.queryAll(By.css('.form__error-message'));
    expect(errors.length).toEqual(0);

    // Common
    //  Label for every item
    const labels = fixture.debugElement.queryAll(By.css('.form__label'));
    expect(labels.length).toEqual(expectedControls.length);
    for (let i = 0; i < expectedControls.length; i++) {
      expect(labels[i].nativeElement.innerText).toEqual(expectedControls[i].label);
      expect(labels[i].nativeElement.getAttribute('for')).toEqual(expectedControls[i].key);
    }

    // Controls
    //  Textbox
    const textbox = fixture.debugElement.query(By.css('.form__textbox')).nativeElement;
    expect(textbox.getAttribute('type')).toEqual('text');
    expect(textbox.getAttribute('id')).toEqual(expectedControls[1].key);
    //    Initial value
    expect(textbox.value).toEqual(expectedControls[1].value);
    //  Dropdown
    const dropdown = fixture.debugElement.query(By.css('.form__dropdown')).nativeElement;
    expect(dropdown.getAttribute('id')).toEqual(expectedControls[0].key);
    //    Drop down options
    expect(dropdown.options.length).toEqual(dropdownOptions.length);
    for (let i = 0; i < dropdownOptions.length; i++) {
      expect(dropdown.options[i].value).toEqual(dropdownOptions[i].value);
    }
    //    Initial value
    expect(dropdown.value).toEqual(expectedControls[0].value);

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
    const textbox = fixture.debugElement.query(By.css('.form__textbox')).nativeElement;
    textbox.dispatchEvent(new Event('input'));

    // trigger initial data binding
    fixture.detectChanges();

    // Textbox
    expect(component.formGroup.controls.productName.value).toEqual(testString);
    // Dropdown
    expect(component.formGroup.controls.logLevel.value).toEqual(testSelection);

    // Value
    expect(component.formGroup.value).toEqual({ logLevel: testSelection, productName: testString });
    // IsDirty = True
    expect(component.isDirty()).toBe(true);
    expect(component.formGroup.dirty).toBe(true);
    // IsValid = True
    expect(component.isValid()).toBe(true);
    expect(component.formGroup.status).toEqual('VALID');
    // Error Message
    const errors = fixture.debugElement.queryAll(By.css('.form__error-message'));
    expect(errors.length).toEqual(0);

  }));

  it('should raise error if required value not provided', fakeAsync(() => {

    // Textbox
    component.formGroup.controls.productName.setValue('');
    //  Trigger event
    const textbox = fixture.debugElement.query(By.css('.form__textbox')).nativeElement;
    textbox.dispatchEvent(new Event('input'));

    // trigger initial data binding
    fixture.detectChanges();

    expect(component.formGroup.controls.productName.value).toEqual('');
    expect(component.formGroup.controls.productName.errors).toEqual({ required: true });

    // IsDirty = True
    expect(component.isDirty()).toBe(true);
    expect(component.formGroup.dirty).toBe(true);
    // IsValid = True
    expect(component.isValid()).toBe(false);
    expect(component.formGroup.status).toEqual('INVALID');
    // Error Message
    const errors = fixture.debugElement.queryAll(By.css('.form__error-message'));
    expect(errors.length).toEqual(1);

    // Save button
    const btnSave = fixture.debugElement.query(By.css('.form__submit'));
    expect(btnSave.nativeElement.disabled).toBe(true);

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
    const textbox = fixture.debugElement.query(By.css('.form__textbox')).nativeElement;
    textbox.dispatchEvent(new Event('input'));

    // Click Save
    fixture.debugElement.query(By.css('form')).triggerEventHandler('ngSubmit', null);

    // trigger initial data binding
    fixture.detectChanges();

    // Check Save is triggered
    expect(eventTriggered).toBe(true);

    // IsDirty = True
    expect(component.isDirty()).toBe(false);
    expect(component.formGroup.dirty).toBe(false);

  }));

});
