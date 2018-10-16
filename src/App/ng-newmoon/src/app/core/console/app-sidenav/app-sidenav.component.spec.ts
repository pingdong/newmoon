import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppSideNavComponent } from './app-sidenav.component';

describe('AppSideComponent', () => {
  let component: AppSideNavComponent;
  let fixture: ComponentFixture<AppSideNavComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppSideNavComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppSideNavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
