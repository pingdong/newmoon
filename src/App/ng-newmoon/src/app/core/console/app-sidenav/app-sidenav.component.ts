import { Component, OnInit, ChangeDetectionStrategy, KeyValueDiffers, DoCheck, ChangeDetectorRef } from '@angular/core';

import { ConfigService } from '../../config/config.service';
import { AppModule } from '../../config/app.module.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-sidenav',
  templateUrl: './app-sidenav.component.html',
  styleUrls: ['./app-sidenav.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppSideNavComponent implements OnInit, DoCheck {

  public modules: AppModule[];

  private differ: any;

  constructor(
    /** @internal */
    private configService: ConfigService,
    private changeDetectorRef: ChangeDetectorRef,
    private differs: KeyValueDiffers
  ) {
    this.differ = this.differs.find({}).create();
  }

  ngOnInit(): void {
    this.configService.getConfig()
          .subscribe((cfg) => this.modules = cfg.modules);
  }

  ngDoCheck() {
    const changed = this.differ.diff(this.modules); // check for changes
    if (changed) {
      this.changeDetectorRef.markForCheck();
    }
  }

}
