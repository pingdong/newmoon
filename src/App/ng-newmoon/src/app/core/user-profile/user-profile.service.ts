import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { UserProfile } from './user-profile.model';
import { CoreModule } from '../core.module';

@Injectable({
  providedIn: CoreModule
})
export class UserProfileService {

  public getProfile(): Observable<UserProfile> {

    const profile: UserProfile = new UserProfile();

    return of(profile);

  }

}
