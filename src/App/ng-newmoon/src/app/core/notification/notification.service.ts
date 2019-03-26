import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subject } from 'rxjs';

import { NotificationMessage } from './notification.message.model';

@Injectable({ providedIn: 'root' })
export class NotificationService {

  private message$ = new Subject<NotificationMessage>();

  constructor(
    /** @internal */
    private snackBar: MatSnackBar,
  ) {
    this.message$
      .subscribe(msg =>
        this.snackBar.open(msg.message, '', {
          duration: 2000,
      })
    );
  }

  public sendText(message: string): void {
    const msg = new NotificationMessage(message);

    this.sendMessage(msg);
  }

  public sendMessage(message: NotificationMessage): void {
    this.message$.next(message);
  }

  public clear(): void {
    this.message$.next();
  }
}
