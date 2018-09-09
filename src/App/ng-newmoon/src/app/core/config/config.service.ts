import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { AddressService } from '../../address.service';
import { AppConfig } from './config.model';

@Injectable()
export class ConfigService {

  constructor(
    private http: HttpClient,
    private address: AddressService
  ) {}

  public getConfig() {
    return this.http.get<AppConfig>(this.address.config)
                    .pipe(
                      retry(3),
                      catchError(this.handleError),
                    );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occured.
      console.error('An error occured:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      console.error(`Backend returned code ${error.status}, body was: ${error.error}`);
    }

    // return an observable with a user-friendly error message.
    return throwError('Something was wrong, please try again.');
  }
}
