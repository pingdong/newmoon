import { Injectable } from '@angular/core';
import { DynamicItemBase, TextItem } from '@app/shared/forms';

@Injectable()
export class UserProfileControlService {

  // Form definition
  public getDefinition(): DynamicItemBase<string>[] {

    const items: DynamicItemBase<string>[] = [

      new TextItem({
        key: 'lastName',
        label: 'Last name',
        value: 'Dong',
        required: true,
        order: 2
      }),

      new TextItem({
        key: 'firstName',
        label: 'First name',
        value: 'Ping',
        required: true,
        order: 1
      }),

      new TextItem({
        key: 'emailAddress',
        label: 'Email',
        inputType: 'email',
        order: 3
      })
    ];

    return items;

  }

}
