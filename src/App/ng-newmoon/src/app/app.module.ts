import { registerLocaleData } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from '@app/core';

import { AppComponent } from './core/console/components/app.component';
import { AppRoutingModule } from './app-routing.module';

// Loading extensions
import '@app/shared/extensions/string-extension.ts';

// Localisation
import localeZhExtra from '@angular/common/locales/extra/zh-Hans';
import localeZh from '@angular/common/locales/zh-Hans';
registerLocaleData(localeZh, 'zh-Hans', localeZhExtra);

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    // If the backend service uses different names for the XSRF token cookie or header
    // HttpClientXsrfModule.withOptions({
    //   cookieName: 'My-Xsrf-Cookie',
    //   headerName: 'My-Xsrf-Header',
    // }),

    CoreModule,
    AppRoutingModule,
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'zh-Hans' },
  ],
  bootstrap: [
    AppComponent,
  ],
})
export class AppModule { }
