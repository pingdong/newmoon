
import { registerLocaleData } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { CoreModule } from './core';

import { AppComponent } from './core';
import { AppRoutingModule } from './app-routing.module';

import './extensions/string-extension.ts';

import localeZhExtra from '@angular/common/locales/extra/zh-Hans';
import localeZh from '@angular/common/locales/zh-Hans';
registerLocaleData(localeZh, 'zh-Hans', localeZhExtra);

@NgModule({
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,

    AppRoutingModule,
    CoreModule,
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'zh-Hans' },
  ],
  bootstrap: [
    AppComponent,
  ],
})
export class AppModule { }
