import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material';

import { UnsaveConfirmComponent } from './unsave-confirm.component';

describe('UnsaveConfirmComponent', () => {
  let component: UnsaveConfirmComponent;
  let fixture: ComponentFixture<UnsaveConfirmComponent>;
  const mockDialogRef = { close: jasmine.createSpy('close') };

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UnsaveConfirmComponent ],
      imports: [ MatDialogModule ],
      providers: [
        {provide: MAT_DIALOG_DATA, useValue: true },
        {provide: MatDialogRef, useValue: mockDialogRef}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnsaveConfirmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  afterEach(() => {
    mockDialogRef.close.calls.reset();
  });

  it('should call close when click Yes', () => {
    component.onYesClick();

    expect(mockDialogRef.close.calls.count()).toBe(1);
    expect(mockDialogRef.close).toHaveBeenCalledWith(true);
  });

  it('should call close when click No', () => {
    component.onNoClick();

    expect(mockDialogRef.close.calls.count()).toBe(1);
    expect(mockDialogRef.close).toHaveBeenCalledWith(false);
  });

});
