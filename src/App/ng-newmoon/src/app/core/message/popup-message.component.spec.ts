import { Router } from '@angular/router';
import { async, ComponentFixture, TestBed, tick, fakeAsync } from '@angular/core/testing';

import { PopupMessageComponent } from './popup-message.component';

describe('Popup Message', () => {

  const routerSpy = jasmine.createSpyObj('Router', ['navigate']);
  routerSpy.navigate.and.stub();

  let component: PopupMessageComponent;
  let fixture: ComponentFixture<PopupMessageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PopupMessageComponent ],
      providers: [
        { provide: Router, useValue: routerSpy },
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PopupMessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should call router in send() after 1ms', fakeAsync(() => {
    routerSpy.navigate.calls.reset();

    component.send();

    fixture.detectChanges();

    expect(routerSpy.navigate.calls.count()).toEqual(0);

    // tslint:disable-next-line:no-magic-numbers
    tick(1000);
    expect(routerSpy.navigate.calls.count()).toEqual(1);
  }));

  it('should call router in Close()', () => {
    routerSpy.navigate.calls.reset();

    component.close();

    fixture.detectChanges();

    expect(routerSpy.navigate.calls.count()).toEqual(1);
  });

});
