import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Subject } from 'rxjs';
import { filter, debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-header-search',
  templateUrl: './app-header-search.component.html',
  styleUrls: ['./app-header-search.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppHeaderSearchComponent implements OnInit {

  private search$: Subject<string>;

  constructor() {
    this.search$ = new Subject();
  }

  ngOnInit() {
    this.search$
      .pipe(
        filter(text => text.length > 2),
        debounceTime(10),
        distinctUntilChanged()
      )
      .subscribe(result => console.log(result));
  }

  public onSearch(term: string): void {
    this.search$.next(term);
  }

}
