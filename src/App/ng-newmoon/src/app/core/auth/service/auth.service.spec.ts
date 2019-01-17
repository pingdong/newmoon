import { TestBed } from '@angular/core/testing';
import { HttpErrorResponse } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { AuthService } from './auth.service';

let httpTestingController: HttpTestingController;

describe('AuthService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ],
      providers: [ AuthService ]
    });

    httpTestingController = TestBed.get(HttpTestingController);
  });

  afterEach(() => {
    // Clean up
    localStorage.removeItem('token');

    httpTestingController.verify();
  });

  it('Should login success', () => {
    const service = TestBed.get(AuthService);

    service.login('user', 'pwd').subscribe(() => {
      expect(localStorage.getItem('token')).toBe('01234');
    }, fail);

    const req = httpTestingController.expectOne(request => request.url === '/login' && request.body.password === 'pwd');
    expect(req.request.method).toEqual('POST');
    req.flush({ 'token': '01234', 'username': 'user' });
  });

  it('Should login failed with wrong password', () => {
    const service = TestBed.get(AuthService);

    service.login('user', 'abc').subscribe(
      () => fail('should have failed with the 401 error'),
      (error: HttpErrorResponse) => {
        // tslint:disable-next-line:no-magic-numbers
        expect(error.status).toEqual(401, 'status');
        expect(error.error).toEqual('Unauthorized', 'message');
      }
    );

    const req = httpTestingController.expectOne('/login');
    expect(req.request.method).toEqual('POST');
    req.flush('Unauthorized', { status: 401, statusText: 'Not Found' });
  });

});
