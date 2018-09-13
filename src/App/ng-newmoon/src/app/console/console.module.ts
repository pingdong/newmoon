import { CommonModule } from '@angular/common';
import { ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';
import { RouterModule } from '@angular/router';

import { MaterialModule } from '../material';

import { AppComponent } from './main/app.component';
import { AppReadyEvent } from './components/app-ready/app-ready.event';
import { AppHeaderComponent } from './components/app-header/app-header.component';
import { AppFooterComponent } from './components/app-footer/app-footer.component';

@NgModule({
    declarations: [
      AppComponent,

      AppHeaderComponent,
      AppFooterComponent,
    ],
    imports: [
      CommonModule,
      RouterModule,

      MaterialModule,
    ],
    providers: [
      AppReadyEvent,
    ]
  })
  export class ConsoleModule {

    public static forRoot(): ModuleWithProviders {
      return {
        ngModule: ConsoleModule,
        providers: [],
      };
    }

    constructor(@Optional() @SkipSelf() parentModule: ConsoleModule) {
      if (parentModule) {
        throw new Error(
          'ConsoleModule is already loaded. Import it in the AppModule only');
      }
    }

  }
