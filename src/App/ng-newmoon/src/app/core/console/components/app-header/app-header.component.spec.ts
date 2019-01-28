import { Component, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { By } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { async, ComponentFixture, TestBed, inject, fakeAsync } from '@angular/core/testing';
import { Store, StoreModule } from '@ngrx/store';
import { of } from 'rxjs/internal/observable/of';

import { ConfigService, AppConfig } from '@app/core/config';
import { NotificationService } from '@app/core/notification';
import * as fromStore from '@app/core/store/app.states';
import { MockStore, provideMockStore } from '@app/dev/testing/testStore';

import { AppHeaderComponent } from './app-header.component';
import { MaterialModule } from '@app/shared';
import { click } from '@app/dev';

@Component({selector: 'app-header-search', template: '<h1>Search</h1>'})
class AppHeaderSearchStubComponent {}

describe('AppHeaderComponent', () => {

  let component: AppHeaderComponent;
  let fixture: ComponentFixture<AppHeaderComponent>;
  let cd: ChangeDetectorRef;
  let store: MockStore<fromStore.AppState>;

  const appConfig: AppConfig = {
    appTitle: 'Test',
    modules: []
  };
  const configServiceSpy = jasmine.createSpyObj('getConfig', ['getConfig']);
  configServiceSpy.getConfig.and.returnValue(of(appConfig));

  const notificationServiceSpy = jasmine.createSpyObj('NotificationService', ['sendText']);
  notificationServiceSpy.sendText.and.stub();

  const routerSpy = jasmine.createSpyObj('Router', ['navigate']);
  routerSpy.navigate.and.stub();

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AppHeaderComponent,
        AppHeaderSearchStubComponent
      ],
      imports: [
        MaterialModule,
        CommonModule,
        StoreModule.forRoot({})
      ],
      providers: [
        { provide: ConfigService, useValue: configServiceSpy },
        { provide: NotificationService, useValue: notificationServiceSpy },
        { provide: Router, useValue: routerSpy },
        provideMockStore()
      ]
    })
    .compileComponents();
  }));

  beforeEach(async(() => {
    fixture = TestBed.createComponent(AppHeaderComponent);
    component = fixture.componentInstance;
    // For testing OnPush
    cd = fixture.componentRef.injector.get(ChangeDetectorRef);
    component.ngOnInit();
    fixture.detectChanges();
  }));

  beforeEach(inject([Store], (testStore: MockStore<fromStore.AppState>) => {
    store = testStore;
    store.setState({
      authState: {
        isAuthenticated: false,
        username: null,
        token: null,
        errorMessage: null
      }
    });
  }));

  afterEach(() => {
    configServiceSpy.getConfig.calls.reset();
    notificationServiceSpy.sendText.calls.reset();
    routerSpy.navigate.calls.reset();

    component.ngOnDestroy();
  });

  it('should init properly', fakeAsync(() => {
    // Initial
    //   Show AppTitle
    //   Is not logged in
    //     app-head-search: invisible
    //     appsetting: invisible
    //     message: invisible
    //     helper: visible
    //     login: visible

    const logoButton = fixture.debugElement.query(By.css('.header__logo__button')).nativeElement;
    expect(logoButton.textContent).toBe(appConfig.appTitle);

    const search = fixture.debugElement.queryAll(By.css('h1'));
    expect(search.length).toBe(0);

    const buttons = fixture.debugElement.queryAll(By.css('button'));
    // tslint:disable-next-line:no-magic-numbers
    expect(buttons.length).toBe(4);  // dehaze, logo, help, login
  }));

  // Side Nav
  //     Click

  // OpenAppSetting
  //    Click

  // Message
  //    Click

  it('should navigate after clicking help', fakeAsync(() => {
    // Help
    //    Click
    const buttons = fixture.debugElement.queryAll(By.css('button'));
    // tslint:disable-next-line:no-magic-numbers
    click(buttons[2]);

    expect(notificationServiceSpy.sendText.calls.count()).toBe(1);
  }));

  // UserProfile
  //    Click

  // Not Login
  //    Click
  //      Show Username
  //        app-head-search: visible
  //        appsetting: visible
  //        message: visible
  //        helper: visible
  //        login: invisible

  // Login
  //    Click
  //        Show Username
  //        app-head-search: visible
  //        appsetting: visible
  //        message: visible
  //        helper: visible
  //        login: invisible

});
