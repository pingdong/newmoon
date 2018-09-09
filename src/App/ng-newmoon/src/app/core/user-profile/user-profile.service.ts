import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { UserProfile } from './user-profile.model';

@Injectable()
export class UserProfileService {

  public getProfile(): Observable<UserProfile> {

    const profile: UserProfile = new UserProfile();

    return of(profile);

  }

}
