import { Observable } from 'rxjs';

export interface UnsaveCheck {
    isDirty: () => Observable<boolean> | Promise<boolean> | boolean;
}
