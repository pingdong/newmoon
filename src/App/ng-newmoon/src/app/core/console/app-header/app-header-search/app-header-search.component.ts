import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { filter, debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-header-search',
  templateUrl: './app-header-search.component.html',
  styleUrls: ['./app-header-search.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppHeaderSearchComponent implements OnInit, OnDestroy {

  private search$: Subject<string>;
  private destoryed$ = new Subject();

  private minimalTextLength = 2;
  private dueTime = 10;

  constructor() {
    this.search$ = new Subject();
  }

  public ngOnInit() {
    this.search$
      .pipe(
        filter(text => text.length > this.minimalTextLength),
        debounceTime(this.dueTime),
        distinctUntilChanged(),
        takeUntil(this.destoryed$)
      )
      .subscribe(result => console.log(result));
  }

  public ngOnDestroy(): void {
    this.destoryed$.next();
  }

  public onSearch(term: string): void {
    this.search$.next(term);
  }

}
