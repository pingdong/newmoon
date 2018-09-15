import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UnsaveConfirmComponent } from './unsave-confirm.component';

describe('UnsaveConfirmComponent', () => {
  let component: UnsaveConfirmComponent;
  let fixture: ComponentFixture<UnsaveConfirmComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UnsaveConfirmComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnsaveConfirmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
