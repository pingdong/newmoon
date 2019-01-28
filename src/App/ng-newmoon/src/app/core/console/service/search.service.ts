import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({providedIn: 'root'})
export class SearchService {

  public search(criteria: string): Observable<any> {
    console.log(criteria);

    return of(true);
  }

}
