[Back](../angular.md)

## OnPush

If the component uses OnPush stratege, you may found after set value to property that marks with @input, component won't work as you expect. 

Calling fixture.changeDetectorRef.markForCheck() doesn't work till this doc is written.

You have to trigger change detection manually.

~~~~
beforeEach(async(() => {
    fixture = TestBed.createComponent(AppSideNavItemComponent);
    .....
    // For testing OnPush
    cd = fixture.componentRef.injector.get(ChangeDetectorRef);
    ....
}));
~~~~

In each test.

~~~~
....
component.InputValue = value;
cd.markForCheck();
fixture.detectChanges();
....
~~~~