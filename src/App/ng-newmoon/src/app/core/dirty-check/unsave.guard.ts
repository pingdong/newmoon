import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { MatDialog } from '@angular/material';
import { Observable } from 'rxjs';
import { first } from 'rxjs/operators';

import { UnsaveCheck } from './unsave.check';
import { UnsaveConfirmComponent } from './unsave-confirm/unsave-confirm.component';

@Injectable()
export class UnsaveGuard implements CanDeactivate<UnsaveCheck> {

  constructor(public dialog: MatDialog) {}

  public canDeactivate(component: UnsaveCheck): Observable<boolean> | boolean {

    if (!component.isDirty) {
      return true;
    }

    if (component.isDirty()) {
      const dialogRef = this.dialog.open(UnsaveConfirmComponent, {
        width: '400px',
        data: false
      });

      return dialogRef.afterClosed().pipe(first());
    } else {
      return true;
    }
  }

}
