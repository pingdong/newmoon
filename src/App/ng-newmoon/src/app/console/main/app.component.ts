import { Component, OnInit } from '@angular/core';

import { AppReadyEvent } from '../components/app-ready/app-ready.event';

@Component({
  selector: 'app-root',
  styleUrls: ['./app.component.css'],
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {

  constructor(
    /** @internal */
    private appReadyEvent: AppReadyEvent,
  ) { }

  public ngOnInit(): void {
    this.appReadyEvent.trigger();
  }

}
