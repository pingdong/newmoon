import { Observable } from 'rxjs';

export interface DataValidation {
    isValid: () => Observable<boolean> | boolean;
}
