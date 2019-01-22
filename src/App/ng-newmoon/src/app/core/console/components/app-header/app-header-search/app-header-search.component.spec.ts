import { async, ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';

import { SearchService } from '@app/core/console';
import { AppHeaderSearchComponent } from './app-header-search.component';

describe('AppHeaderSearchComponent', () => {

  let component: AppHeaderSearchComponent;
  let fixture: ComponentFixture<AppHeaderSearchComponent>;

  const searchSpy = jasmine.createSpyObj('SearchService', ['search']);
  searchSpy.search.and.stub();

  const pauseTime = 500;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppHeaderSearchComponent ],
      providers: [
        { provide: SearchService, useValue: searchSpy }
      ]
    })
    .compileComponents();
  }));

  beforeEach(async(() => {
    fixture = TestBed.createComponent(AppHeaderSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  afterEach(() => {
    searchSpy.search.calls.reset();

    component.ngOnDestroy();
  });

  it('should not call service if criteria short than 3', fakeAsync(() => {
    component.onSearch('a');
    component.onSearch('ab');

    tick(pauseTime);

    expect(searchSpy.search.calls.count()).toBe(0);
  }));

  it('should call service if criteria longer than 3', fakeAsync(() => {
    component.onSearch('a');
    component.onSearch('ab');
    component.onSearch('abc');

    tick(pauseTime);

    expect(searchSpy.search.calls.count()).toBe(1);
    expect(searchSpy.search.calls.mostRecent().args[0]).toBe('abc');
  }));

  it('should call service one if same requests submits', fakeAsync(() => {
    component.onSearch('abc');
    component.onSearch('abc');

    tick(pauseTime);

    expect(searchSpy.search.calls.count()).toBe(1);
    expect(searchSpy.search.calls.mostRecent().args[0]).toBe('abc');
  }));

});
