
import { registerLocaleData } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './console';

import { ConsoleModule } from './console';
import { CoreModule } from './core';
import { ShareModule } from './share';
import { SystemModule } from './system';

import { AppRoutingModule } from './app-routing.module';

import localeZhExtra from '@angular/common/locales/extra/zh-Hans';
import localeZh from '@angular/common/locales/zh-Hans';
registerLocaleData(localeZh, 'zh-Hans', localeZhExtra);

@NgModule({
  imports: [
    BrowserModule,
    HttpClientModule,

    AppRoutingModule,

    CoreModule,
    ShareModule,
    SystemModule, // must be immidately before ConsoleModule
    ConsoleModule, // must be on the last
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'zh-Hans' },
  ],
  bootstrap: [
    AppComponent,
  ],
})
export class AppModule { }