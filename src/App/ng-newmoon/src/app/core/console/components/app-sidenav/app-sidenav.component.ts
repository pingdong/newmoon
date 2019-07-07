import {
  Component,
  OnInit,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  OnDestroy
} from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { ConfigService, AppModule } from '@app/core/config';

@Component({
  selector: 'app-sidenav',
  templateUrl: './app-sidenav.component.html',
  styleUrls: ['./app-sidenav.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppSideNavComponent implements OnInit, OnDestroy {

  public modules: AppModule[];

  private destoryed$ = new Subject();

  constructor(
    /** @internal */
    private configService: ConfigService,
    private changeDetectorRef: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.configService.getConfig()
          .pipe(
            takeUntil(this.destoryed$)
          )
          .subscribe((cfg) => {
              this.modules = cfg.modules;

              this.changeDetectorRef.markForCheck();
            }
          );
  }

  public ngOnDestroy(): void {
    this.destoryed$.next();
  }

  public trackByTitle(module: AppModule) {
    return module.title;
  }

}
