import { TestBed, async, inject } from '@angular/core/testing';
import { of, Observable } from 'rxjs';
import { MatDialog } from '@angular/material';
import { UnsaveGuard } from './unsave.guard';

export class MatDialogMock {
  clickYes: boolean;
  open() {
    return {
      afterClosed: () => of(this.clickYes)
    };
  }
}

describe('Should be able to deactive', () => {

  const unsaveCheckSpy = jasmine.createSpyObj('UnsaveCheck', ['isDirty']);
  const wrongUnsaveCheckSpy = jasmine.createSpyObj('UnsaveCheck', ['dirty']);

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
      ],
      declarations: [
      ],
      providers: [
        { provide: MatDialog, useClass: MatDialogMock }
      ]
    })
    .compileComponents();
  }));

  afterEach(() => {
    unsaveCheckSpy.isDirty.calls.reset();
    wrongUnsaveCheckSpy.dirty.calls.reset();
  });

  it('should be able to deactive if losting data is allowed',
    async(inject([MatDialog], (dialog) => {
      dialog.clickYes = true;
      unsaveCheckSpy.isDirty.and.returnValue(true);

      const guard = new UnsaveGuard(dialog);

      const canDeactivate$ = <Observable<boolean>>guard.canDeactivate(unsaveCheckSpy);
      canDeactivate$.subscribe((deactivate) => {
        expect(deactivate).toBe(true);
      });

      expect(unsaveCheckSpy.isDirty.calls.count()).toBe(1);
    })
  ));

  it('should be able to deactive if losting data is not allowed',
    async(inject([MatDialog], (dialog) => {
      dialog.clickYes = false;
      unsaveCheckSpy.isDirty.and.returnValue(true);

      const guard = new UnsaveGuard(dialog);

      const canDeactivate$ = <Observable<boolean>>guard.canDeactivate(unsaveCheckSpy);
      canDeactivate$.subscribe((deactivate) => {
        expect(deactivate).toBe(false);
      });

      expect(unsaveCheckSpy.isDirty.calls.count()).toBe(1);
    })
  ));

  it('should be able to deactive if all data are saved',
    async(inject([MatDialog], (dialog) => {
      const guard = new UnsaveGuard(dialog);
      unsaveCheckSpy.isDirty.and.returnValue(false);

      expect(guard.canDeactivate(unsaveCheckSpy)).toBe(true);
      expect(unsaveCheckSpy.isDirty.calls.count()).toBe(1);
    })
  ));

  it('should be able to deactive if not implement UnsaveCheck',
    async(inject([MatDialog], (dialog) => {
      const guard = new UnsaveGuard(dialog);
      wrongUnsaveCheckSpy.dirty.and.returnValue(false);

      expect(guard.canDeactivate(wrongUnsaveCheckSpy)).toBe(true);
      expect(wrongUnsaveCheckSpy.dirty).not.toHaveBeenCalled();
    })
  ));

});
