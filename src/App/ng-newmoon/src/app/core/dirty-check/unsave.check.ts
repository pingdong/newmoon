import { Observable } from 'rxjs';

export interface UnsaveCheck {
    isUnsave: () => Observable<boolean> | boolean;
}
