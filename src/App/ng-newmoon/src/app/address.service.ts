import { Injectable } from '@angular/core';

@Injectable({providedIn: 'root'})
export class AddressService {
    public readonly config: string = '/assets/config.json';
}
