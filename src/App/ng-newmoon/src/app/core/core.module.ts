import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule, Optional, SkipSelf } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';

import { SharedModule } from '@app/shared';
import { environment } from '@env/environment.prod';

import { AppComponent } from './console/app.component';
import { AppFooterComponent } from './console/app-footer/app-footer.component';
import { AppHeaderComponent } from './console/app-header/app-header.component';
import { AppHeaderSearchComponent } from './console/app-header/app-header-search/app-header-search.component';
import { AppSideNavComponent } from './console/app-sidenav/app-sidenav.component';
import { AppSideNavItemComponent } from './console/app-sidenav/app-sidenav-item/app-sidenav-item.component';

import { LoginComponent } from './auth/components/login.component';
import { PageNotFoundComponent } from './router/components/page-not-found.component';
import { UnsaveConfirmComponent } from './router/guards/dirty-check/components/unsave-confirm.component';

import { PopupMessageComponent } from './message/popup-message.component';

import { TokenInterceptor } from './http/token.interceptor';
import { ErrorInterceptor } from './http/error.interceptor';

import { reducers } from './store/app.states';
import { AuthEffects } from './auth/store/effects/auth.effects';

import { DevCoreModule } from './dev.core.module';

@NgModule({
    declarations: [
      PageNotFoundComponent,
      LoginComponent,
      UnsaveConfirmComponent,

      PopupMessageComponent,

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
      ReactiveFormsModule,

      SharedModule,

      StoreModule.forRoot(reducers, { }),
      EffectsModule.forRoot([AuthEffects]),

      !environment.production ? StoreDevtoolsModule.instrument({ maxAge: 25 }) : [],
      !environment.production ? DevCoreModule.forRoot() : [],
    ],
    providers: [
      { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
      { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
    ],
    exports: [
      CommonModule,
      RouterModule,
      FormsModule,
      ReactiveFormsModule,

      SharedModule,
    ],
    entryComponents: [
      UnsaveConfirmComponent
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
