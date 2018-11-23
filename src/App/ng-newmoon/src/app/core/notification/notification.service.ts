import { Injectable, OnDestroy, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subject } from 'rxjs';

import { NotificationMessage } from './notification.message.model';
import { takeUntil } from 'rxjs/operators';

@Injectable({providedIn: 'root'})
export class NotificationService implements OnInit, OnDestroy {

  private message$: Subject<NotificationMessage>;
  private destoryed$ = new Subject();

  constructor(
    /** @internal */
    private snackBar: MatSnackBar,
  ) { }

  public ngOnInit() {
    this.message$ = new Subject<NotificationMessage>();

    this.message$
      .pipe(
        takeUntil(this.destoryed$)
      )
      .subscribe(msg =>
        this.snackBar.open(msg.message, '', {
          duration: 2000,
      })
    );
  }

  public ngOnDestroy(): void {
    this.destoryed$.next();
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
