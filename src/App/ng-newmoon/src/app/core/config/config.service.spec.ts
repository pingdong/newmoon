import { TestBed } from '@angular/core/testing';
import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';

import { AddressService } from './address.service';
import { ConfigService } from './config.service';

// The traditional beforeEach() style
// An alternative approach is in auth.service.spec.ts

let httpSpy: jasmine.SpyObj<HttpClient>;

describe('ConfigService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        ConfigService,
        AddressService,
        { provide: HttpClient, useValue: jasmine.createSpyObj('Http', ['get']) },
      ]
    });

    httpSpy = TestBed.get(HttpClient);
  });

  it('Should return default AppConfig', (done: DoneFn) => {
    const service = TestBed.get(ConfigService);
    const stubValue = {  'appTitle': 'Newmoon - NG', 'modules': [ { 'title': 'Dashboard', 'uri': '/dashboard' } ] };

    httpSpy.get.and.returnValue(of(stubValue));

    service.getConfig().subscribe(value => {
      expect(value).toBe(stubValue);
      expect(httpSpy.get.calls.count()).toBe(1, 'spy method was called more than once');

      done();
    });
  });

});
