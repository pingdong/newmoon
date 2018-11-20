import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

import { SelectivePreloadingStrategy } from '../../../shared';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  styleUrls: ['./dashboard.component.css'],
  templateUrl: './dashboard.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardComponent implements OnInit {

  public modules: string[];
  public componenttime = Date.now();
  public preloadtime: number;

  constructor(
    /** @internal **/
    private preloadStrategy: SelectivePreloadingStrategy,
    private route: ActivatedRoute
  ) {}

  public ngOnInit() {
    this.modules = this.preloadStrategy.preloadedModules;

    // Reading prefetch data
    this.route.data
      .subscribe((data: { resolve: number }) => {
        this.preloadtime = data.resolve;
      });
  }

}
