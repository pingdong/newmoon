import { TestBed, async } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

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
