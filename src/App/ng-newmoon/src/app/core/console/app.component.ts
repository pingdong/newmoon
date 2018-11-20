import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

import { AppReadyEvent } from './app-ready/app-ready.event';

@Component({
  selector: 'app-root',
  styleUrls: ['./app.component.css'],
  templateUrl: './app.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent implements OnInit {

  public isSidePanelOpened = true;

  constructor(
    /** @internal */
    private appReadyEvent: AppReadyEvent,
  ) { }

  public ngOnInit(): void {
    this.appReadyEvent.trigger();
  }

  public sidenavTrigger() {
    this.isSidePanelOpened = !this.isSidePanelOpened;
  }
}
