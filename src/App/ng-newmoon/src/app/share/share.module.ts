import { CommonModule } from '@angular/common';
import { ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { LoginComponent } from './login/login.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

@NgModule({
  declarations: [
    PageNotFoundComponent,
    LoginComponent,
  ],
  imports: [
    CommonModule,

    ReactiveFormsModule
  ],
  providers: [
  ],
})
export class ShareModule {

  public static forRoot(): ModuleWithProviders {
    return {
      ngModule: ShareModule,
      providers: [],
    };
  }

  constructor(@Optional() @SkipSelf() parentModule: ShareModule) {
    if (parentModule) {
      throw new Error(
        'SystemModule is already loaded. Import it in the AppModule only');
    }
  }

}
