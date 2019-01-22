import { TestBed, async } from '@angular/core/testing';
import { HttpErrorResponse } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { SearchService } from './search.service';

describe('SearchService', () => {

  let service: SearchService;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ],
      providers: [ SearchService ]
    });
  }));

  beforeEach(async(() => {
    service = TestBed.get(SearchService);
  }));

  it('Should create', () => {
    expect(service).toBeTruthy();
  });

});
