import { APP_BASE_HREF } from '@angular/common';
import { ChangeDetectorRef } from '@angular/core';
import { By } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material';
import { async, ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';

import { AppModule } from '@app/core/config/app.module.model';
import { AppSideNavItemComponent } from './app-sidenav-item.component';
import { click } from '@app/dev';

describe('AppSideNavItemComponent', () => {

  let component: AppSideNavItemComponent;
  let fixture: ComponentFixture<AppSideNavItemComponent>;
  let cd: ChangeDetectorRef;

  const pauseTime = 500;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        MatIconModule,
        RouterModule.forRoot([])
      ],
      declarations: [
        AppSideNavItemComponent
      ],
      providers: [
        { provide: APP_BASE_HREF, useValue : '/' }
      ]
    })
    .compileComponents();
  }));

  beforeEach(async(() => {
    fixture = TestBed.createComponent(AppSideNavItemComponent);
    component = fixture.componentInstance;
    // For testing OnPush
    cd = fixture.componentRef.injector.get(ChangeDetectorRef);
    fixture.detectChanges();
  }));

  it('should show item properly without submodules', fakeAsync(() => {
    const config: AppModule = {
      title: 'Test',
      uri: 'LINK',
      modules: []
    };

    component.module = config;
    component.ngOnInit();
    cd.markForCheck();
    fixture.detectChanges();

    expect(component.isExpanded).toBe(false);
    expect(component.hasSubmodules).toBe(false);

    // Should show its module.title
    //   title and its url
    // Should not show submodule area
    const elements = fixture.debugElement.queryAll(By.css('a'));
    expect(elements.length).toBe(1);

    const link = elements[0].nativeElement;
    expect(link.textContent).toBe(config.title);
    expect(link.href).toContain(`/${config.uri}`);
  }));

  it('should show item properly with submodules', fakeAsync(() => {
    const config: AppModule = {
      title: 'Test',
      uri: 'LINK',
      modules: [
        {
          title: 'Module 1',
          uri: 'M1',
          modules: []
        },
        {
          title: 'Module 2',
          uri: 'M2',
          modules: []
        }
      ]
    };

    component.module = config;
    component.ngOnInit();
    cd.markForCheck();
    fixture.detectChanges();

    expect(component.isExpanded).toBe(false);
    expect(component.hasSubmodules).toBe(true);

    // Should not show module title area
    // Should module title
    // isExpand default false
    //    arrow-down is visible
    //    arrow-up is invisible
    const button = fixture.nativeElement.querySelector('button');
    expect(button.textContent).toContain(config.title);
    expect(button.textContent).toContain('keyboard_arrow_down');
    expect(button.textContent).not.toContain('keyboard_arrow_up');

    const elements = fixture.nativeElement.querySelectorAll('a');
    expect(elements.length).toBe(0);
  }));

  it('should expand menu with submodules', fakeAsync(() => {
    const config: AppModule = {
      title: 'Test',
      uri: 'LINK',
      modules: [
        {
          title: 'Module 1',
          uri: 'M1',
          modules: []
        },
        {
          title: 'Module 2',
          uri: 'M2',
          modules: []
        }
      ]
    };

    component.module = config;
    component.ngOnInit();
    cd.markForCheck();
    fixture.detectChanges();

    const button = fixture.nativeElement.querySelector('button');
    click(button);
    tick(pauseTime);
    fixture.detectChanges();

    expect(component.isExpanded).toBe(true);

    // isExpand change to true after toggle
    //  arrow-down is invisible
    //  arrow-up is visible
    //  submodules list
    expect(button.textContent).toContain(config.title);
    expect(button.textContent).toContain('keyboard_arrow_up');
    expect(button.textContent).not.toContain('keyboard_arrow_down');

    const elements = fixture.nativeElement.querySelectorAll('a');
    expect(elements.length).toBe(config.modules.length);

    const link1 = elements[0];
    expect(link1.text).toBe(config.modules[0].title);
    expect(link1.href).toContain(`/${config.modules[0].uri}`);

    const link2 = elements[1];
    expect(link2.text).toBe(config.modules[1].title);
    expect(link2.href).toContain(`/${config.modules[1].uri}`);
  }));

});
