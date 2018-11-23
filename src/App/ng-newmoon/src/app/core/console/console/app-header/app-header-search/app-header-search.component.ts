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

  constructor() {
    this.search$ = new Subject();
  }

  public ngOnInit() {
    this.search$
      .pipe(
        filter(text => text.length > 2),
        debounceTime(10),
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
