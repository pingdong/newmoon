import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppSideNavItemComponent } from './app-sidenav-item.component';

describe('AppSideNavItemComponent', () => {
  let component: AppSideNavItemComponent;
  let fixture: ComponentFixture<AppSideNavItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppSideNavItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppSideNavItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
