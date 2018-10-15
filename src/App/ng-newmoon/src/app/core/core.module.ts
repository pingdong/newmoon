import { CommonModule } from '@angular/common';
import { NgModule, Optional, SkipSelf } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from '../shared';

import { AppComponent } from './console/app.component';

import { LoginComponent } from './auth/component/login.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

import { AppFooterComponent } from './console/app-footer/app-footer.component';
import { AppHeaderComponent } from './console/app-header/app-header.component';
import { AppHeaderSearchComponent } from './console/app-header/app-header-search/app-header-search.component';
import { AppSideNavComponent } from './console/app-sidenav/app-sidenav.component';
import { AppSideNavItemComponent } from './console/app-sidenav/app-sidenav-item/app-sidenav-item.component';

@NgModule({
    declarations: [
      PageNotFoundComponent,
      LoginComponent,

      AppComponent,
      AppHeaderComponent,
      AppHeaderSearchComponent,
      AppFooterComponent,
      AppSideNavComponent,
      AppSideNavItemComponent,
    ],
    imports: [
      CommonModule,
      RouterModule,
      FormsModule,
      ReactiveFormsModule,

      MaterialModule,
    ]
  })
  export class CoreModule {

    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
      if (parentModule) {
        throw new Error(
          'CoreModule is already loaded. Import it in the AppModule only');
      }
    }

  }
