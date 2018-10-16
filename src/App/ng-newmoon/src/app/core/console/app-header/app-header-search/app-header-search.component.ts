import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { filter, debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-header-search',
  templateUrl: './app-header-search.component.html',
  styleUrls: ['./app-header-search.component.css']
})
export class AppHeaderSearchComponent implements OnInit {

  public term: string;

  private search$: Subject<string>;

  constructor() {
    this.search$ = new Subject();
  }

  ngOnInit() {
    this.search$.pipe(
                    filter(text => text.length > 2),
                    debounceTime(10),
                    distinctUntilChanged()
                  ).subscribe(x =>
                    console.log(x)
                  );
  }

  public onSearch() {
    this.search$.next(this.term);
  }

}
