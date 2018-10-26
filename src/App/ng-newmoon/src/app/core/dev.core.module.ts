import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule, ModuleWithProviders } from '@angular/core';

import { DevInterceptor } from './http/dev.interceptor';

@NgModule({
    declarations: [
    ],
    imports: [
    ],
    providers: [
    ],
    exports: [
    ]
  })
  export class DevCoreModule {

    public static forRoot(): ModuleWithProviders {
        return {
          ngModule: DevCoreModule,
          providers: [
            { provide: HTTP_INTERCEPTORS, useClass: DevInterceptor, multi: true }
          ],
        };
      }

  }
