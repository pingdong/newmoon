import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';

import { AppFooterComponent } from './app-footer.component';

describe('AppFooterComponent', () => {
  let component: AppFooterComponent;
  let fixture: ComponentFixture<AppFooterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppFooterComponent ],
    })
    .compileComponents();
  }));

  beforeEach(async(() => {
    fixture = TestBed.createComponent(AppFooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should has correct year', () => {
    const currentYear = (new Date()).getFullYear();

    expect(component.currentYear).toBe(currentYear, 'Value doesn\'t match');

    const copyright = fixture.debugElement.query(By.css('.footer__copyright'));
    expect(copyright.nativeElement.innerText).toEqual(`Ping Dong © 2018 - ${currentYear}`, 'Copyright doesn\'t match its expectation');
  });

});
