import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppHeaderSearchComponent } from './app-header-search.component';

describe('AppHeaderSearchComponent', () => {
  let component: AppHeaderSearchComponent;
  let fixture: ComponentFixture<AppHeaderSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppHeaderSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppHeaderSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
