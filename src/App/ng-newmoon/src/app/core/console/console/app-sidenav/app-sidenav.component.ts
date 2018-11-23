import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';

import { ConfigService, AppModule } from '@app/core/config';

@Component({
  selector: 'app-sidenav',
  templateUrl: './app-sidenav.component.html',
  styleUrls: ['./app-sidenav.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppSideNavComponent implements OnInit {

  public modules: AppModule[];

  constructor(
    /** @internal */
    private configService: ConfigService,
    private changeDetectorRef: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.configService.getConfig()
          .subscribe((cfg) => {
              this.modules = cfg.modules;

              this.changeDetectorRef.markForCheck();
            }
          );
  }

}
