import { Inject, Injectable } from '@angular/core';
import { DOCUMENT } from '@angular/common';

// tslint:disable-next-line:max-line-length
// https://www.bennadel.com/blog/3151-revisited-creating-an-event-driven-pre-bootstrap-loading-screen-in-angular-2-0-0.htm
// To provide pre-bootstrap loading state

@Injectable({providedIn: 'root'})
export class AppReadyEvent {

  private isAppReady = false;

  constructor( @Inject(DOCUMENT) private document: Document) { }

  // I trigger the 'appready' event.
  // --
  // NOTE: In this particular implementation of this service on this PLATFORM, this
  // simply triggers the event on the DOM (Document Object Model); however, one could
  // easily imagine this event being triggered on an Observable or some other type of
  // message transport that makes more sense for a different platform. Nothing about
  // the DOM-interaction leaks outside of this service.
  public trigger() {
    // If the app-ready event has already been triggered, just ignore any subsequent
    // calls to trigger it again.
    if (this.isAppReady) {
        return;
    }

    const bubbles = true;
    const cancelable = false;

    // const appReadyEvent = new CustomEvent('appready', { bubbles, cancelable });
    this.document.dispatchEvent( this.createEvent('appready', bubbles, cancelable) );
    this.isAppReady = true;
  }

  // I create and return a custom event with the given configuration.

  private createEvent(eventType: string, bubbles: boolean, cancelable: boolean): Event {

    // IE (shakes fist) uses some other kind of event initialization. As such,
    // we'll default to trying the "normal" event generation and then fallback to
    // using the IE version.

    let customEvent: CustomEvent;

    try {
      customEvent = new CustomEvent(eventType, { bubbles, cancelable });
    } catch ( error ) {
        customEvent = this.document.createEvent('CustomEvent');
        customEvent.initCustomEvent( eventType, bubbles, cancelable, null);
    }

    return (customEvent);
  }
}
