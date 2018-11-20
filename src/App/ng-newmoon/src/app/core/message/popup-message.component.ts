import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-popup-message',
  templateUrl: './popup-message.component.html',
  styleUrls: ['./popup-message.component.css']
})
export class PopupMessageComponent {

  constructor(private router: Router) {}

  send() {
    setTimeout(() => {
      this.closePopup();
    }, 1000);
  }

  close() {
    this.closePopup();
  }

  private closePopup(): void {
    // Providing a `null` value to the named outlet
    // clears the contents of the named outlet
    this.router.navigate([{ outlets: { popup: null }}]);
  }
}
