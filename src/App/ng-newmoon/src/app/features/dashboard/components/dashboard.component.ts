import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SelectivePreloadingStrategy } from '@app/shared/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-dashboard',
  styleUrls: ['./dashboard.component.css'],
  templateUrl: './dashboard.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardComponent implements OnInit, OnDestroy {

  public modules: string[];
  public componenttime = Date.now();
  public preloadtime: number;

  private destoryed$ = new Subject();

  constructor(
    /** @internal **/
    private preloadStrategy: SelectivePreloadingStrategy,
    private route: ActivatedRoute
  ) {}

  public ngOnInit() {
    this.modules = this.preloadStrategy.preloadedModules;

    // Reading prefetch data
    this.route.data
      .pipe (
        takeUntil(this.destoryed$)
      )
      .subscribe((data: { resolve: number }) => {
        this.preloadtime = data.resolve;
      });
  }

  public ngOnDestroy(): void {
    this.destoryed$.next();
  }

}
