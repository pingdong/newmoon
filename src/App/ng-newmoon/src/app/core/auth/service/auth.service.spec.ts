import { of } from 'rxjs';

import { AuthService } from './auth.service';

describe('AuthService', () => {

  // Clean up
  afterEach(() => {
    localStorage.removeItem('token');
  });

  function setup() {
    const httpSpy = jasmine.createSpyObj('Http', ['post']);

    const stubValue = { 'token': '01234', 'username': 'user' };
    httpSpy.post.withArgs('/login', { 'username': 'user', 'password': 'pwd' }).and.returnValue(of(stubValue))
                .and.returnValue(of({}));

    const authService = new AuthService(httpSpy);

    return { authService, httpSpy };
  }

  it('Should login success', (done: DoneFn) => {
    const { authService, httpSpy } = setup();

    authService.login('user', 'pwd').subscribe(value => {
      expect(httpSpy.post.calls.count()).toBe(1, 'spy method was called more than once');

      done();
    });

    expect(localStorage.getItem('token')).toBe('01234');
  });

  it('Should login failed with wrong password', (done: DoneFn) => {
    const { authService, httpSpy } = setup();

    authService.login('user', 'abc').subscribe(value => {
      expect(httpSpy.post.calls.count()).toBe(1, 'spy method was called more than once');

      done();
    });

    expect(localStorage.getItem('token')).toBeNull();
  });

});
