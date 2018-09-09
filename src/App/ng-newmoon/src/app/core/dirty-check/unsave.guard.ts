import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { UnsaveCheck } from './unsave.check';

@Injectable()
export class UnsaveGuard implements CanDeactivate<UnsaveCheck> {

  public canDeactivate(component: UnsaveCheck): Observable<boolean> | boolean {
        return component.isUnsave ? component.isUnsave() : true;
    }

}
