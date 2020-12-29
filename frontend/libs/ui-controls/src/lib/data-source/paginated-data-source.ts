import { BehaviorSubject, combineLatest, Observable, Subject } from 'rxjs';
import { distinctUntilChanged, map, shareReplay, startWith, switchMap } from 'rxjs/operators';
import { Page } from './page';
import { PaginatedEndpoint } from './paginated-endpoint';
import { SimpleDataSource } from './simple-data-source';
import { Sort } from './sort';

export class PaginatedDataSource<T> implements SimpleDataSource<T> {
  private pageNumber = new Subject<number>();
  private sort = new Subject<Sort<T>>();

  private pageSize = new BehaviorSubject<number>(10);

  public page$: Observable<Page<T>>;

  constructor(endpoint: PaginatedEndpoint<T>, initialSort: Sort<T>, size = 10) {
    const param$ = combineLatest([this.pageSize.pipe(distinctUntilChanged()), this.sort.pipe(startWith(initialSort))]);

    this.page$ = param$.pipe(
      switchMap(([pageSize, sort]) =>
        this.pageNumber.pipe(
          startWith(1),
          switchMap(page => endpoint({ page, sort, size: pageSize }))
        )
      ),
      shareReplay(1)
    );
  }

  public setPageSize(size: number): void {
    this.pageSize.next(size);
  }

  public sortBy(sort: Sort<T>): void {
    this.sort.next(sort);
  }

  public fetch(page: number): void {
    this.pageNumber.next(page);
  }

  public connect(): Observable<T[]> {
    return this.page$.pipe(map(x => x.items));
  }

  public disconnect(): void {
    // nothing to do
  }
}
