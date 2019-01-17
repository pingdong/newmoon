import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { AddressService } from './address.service';
import { ConfigService } from './config.service';

// The traditional beforeEach() style
// An alternative approach is in auth.service.spec.ts

let httpTestingController: HttpTestingController;

describe('ConfigService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ],
      providers: [
        AddressService,
        ConfigService
      ]
    });

    httpTestingController = TestBed.get(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('Should return default AppConfig', () => {
    const service = TestBed.get(ConfigService);
    const returnValue = {  'appTitle': 'Newmoon - NG', 'modules': [ { 'title': 'Dashboard', 'uri': '/dashboard' } ] };

    service.getConfig().subscribe(value => {
      expect(value).toBe(returnValue);
    }, fail);

    const req = httpTestingController.expectOne('/assets/config.json');
    expect(req.request.method).toEqual('GET');
    req.flush(returnValue);
  });

});
