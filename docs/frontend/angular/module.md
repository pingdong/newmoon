[Back](../angular.md)

## Best Practices

1. Best practices for architecture with Core, Shared and lazy-loaded Feature modules
2. Using aliases for app and environments folders to support cleaner imports

### Core

All services which have to have one and only one instance per application (singleton services) should be implemented here. Typical example can be authentication service or user service. 

### Shared

All the “dumb” components and pipes should be implemented here. These components don’t import and inject services from core or other features in their constructors. They should receive all data though attributes in the template of the component using them. This all sums up to the fact that SharedModule doesn’t have any dependency to the rest of our application.

It is also the perfect place to import and re-export Angular Material components.

### Features

Multiple feature modules are for every independent feature of our application. Feature modules should only import services from CoreModule. If feature module A needs to import service from feature module B consider moving that service into core.

Rule of thumb is to try to create features which don’t depend on any other features just on services provided by CoreModule and components provided by SharedModule.

### LazyLoading

We should lazy load our feature modules whenever possible. Theoretically only one feature module should be loaded synchronously during the app startup to show initial content. Every other feature module should be loaded lazily after user triggered navigation.

### Alias Import 

Aliasing our app and environments folders will enable us to implement clean imports which will be consistent throughout our application.