import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  styleUrls: ['./app-footer.component.css'],
  templateUrl: './app-footer.component.html',
})
export class AppFooterComponent {

  public currentYear = (new Date()).getFullYear();

  constructor(
  ) { }
}
