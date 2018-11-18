import { registerLocaleData } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent, CoreModule } from './core';
import { SharedModule } from './shared';

import './extensions/string-extension.ts';

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
    SharedModule.forRoot(),

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
