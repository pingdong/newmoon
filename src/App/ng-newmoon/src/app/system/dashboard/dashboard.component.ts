import { Component, OnInit } from '@angular/core';

import { SelectivePreloadingStrategy } from '../../core';

@Component({
  selector: 'app-dashboard',
  styleUrls: ['./dashboard.component.css'],
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {

  public modules: string[];
  public today = Date.now();

  constructor(
    /** @internal **/
    private preloadStrategy: SelectivePreloadingStrategy
  ) {}

  public ngOnInit() {
    this.modules = this.preloadStrategy.preloadedModules;
  }

}
