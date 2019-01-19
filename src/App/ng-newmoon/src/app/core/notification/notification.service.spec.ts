import { NotificationService } from './notification.service';
import { fakeAsync, tick } from '@angular/core/testing';

import { NotificationMessage } from './notification.message.model';

describe('NotificationService', () => {

  let service: NotificationService;
  const snackBar = jasmine.createSpyObj('MatSnackBar', ['open']);

  beforeEach(() => {
    service = new NotificationService(snackBar);
  });

  afterEach(() => {
    snackBar.open.calls.reset();
  });

  it('Should send string message properly', fakeAsync(() => {
    const msgText = 'Testing message';

    service.sendText(msgText);

    tick(100);

    expect(snackBar.open.calls.count()).toEqual(1);
  }));

  it('Should send message properly', fakeAsync(() => {
    const msg = new NotificationMessage('Testing message');

    service.sendMessage(msg);

    tick(100);

    expect(snackBar.open.calls.count()).toEqual(1);
  }));

});
