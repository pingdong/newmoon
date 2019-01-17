import { Router } from '@angular/router';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupMessageComponent } from './popup-message.component';

describe('Popup Message', () => {

  const routerSpy = jasmine.createSpyObj('Router', ['navigate']);

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

  it('should call router in Close()', () => {
    routerSpy.navigate.and.stub();

    component.close();

    fixture.detectChanges();

    expect(routerSpy.navigate.calls.count()).toEqual(1);
  });

});
