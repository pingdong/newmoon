import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DynamicSettingComponent } from './dynamic-form.component';

describe('DynamicSettingComponent', () => {
  let component: DynamicSettingComponent;
  let fixture: ComponentFixture<DynamicSettingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DynamicSettingComponent ],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DynamicSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
