import { Component, OnInit, Input, ChangeDetectionStrategy } from '@angular/core';

import { AppModule } from '../../../config/app.module.model';

@Component({
  selector: 'app-sidenav-item',
  templateUrl: './app-sidenav-item.component.html',
  styleUrls: ['./app-sidenav-item.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppSideNavItemComponent implements OnInit {

  @Input() module: AppModule;

  public isExpanded: boolean;
  public hasSubmodules: boolean;

  constructor() { }

  ngOnInit() {
    this.hasSubmodules = this.module.modules && this.module.modules.length > 0;
  }

  public toggle(): void {
    this.isExpanded = !this.isExpanded;
  }

}
