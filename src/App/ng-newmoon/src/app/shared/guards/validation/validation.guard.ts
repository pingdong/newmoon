import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';

import { DataValidation } from './data.validation';

@Injectable()
export class ValidationGuard implements CanDeactivate<DataValidation> {

  public canDeactivate(component: DataValidation): Observable<boolean> | boolean {
        return component.isValid ? component.isValid() : true;
    }

}
