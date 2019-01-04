import { TestBed } from '@angular/core/testing';
import { UserProfileService } from './user-profile.service';
import { UserProfile } from './user-profile.model';

describe('UserProfileService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({ providers: [UserProfileService] });
  });

  it('Should return default UserProfile', (done: DoneFn) => {
    const service = TestBed.get(UserProfileService);

    service.getProfile().subscribe(value => {
      expect(value).toEqual(new UserProfile());
      done();
    });

  });

});
