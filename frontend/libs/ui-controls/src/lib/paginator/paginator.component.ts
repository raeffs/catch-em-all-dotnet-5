import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { createPagination, PaginatedDataSource, Pagination } from '@raeffs/data-source';
import { filterNullAndUndefined } from '@raeffs/rxjs';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'cea-paginator',
  templateUrl: 'paginator.component.html',
  styleUrls: ['paginator.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PaginatorComponent {
  private readonly _dataSource: BehaviorSubject<PaginatedDataSource<
    unknown
  > | null> = new BehaviorSubject<PaginatedDataSource<unknown> | null>(null);

  @Input()
  public set dataSource(value: PaginatedDataSource<unknown>) {
    this._dataSource.next(value);
  }

  constructor(private readonly router: Router) {}

  public readonly pagination: Observable<Pagination> = this._dataSource.pipe(
    filterNullAndUndefined(),
    switchMap(source => source.data),
    map(page => createPagination(page))
  );

  public goToPage(newPage: number): void {
    //this._dataSource.value?.changePageNumber(newPage);
    this.router.navigate([], { queryParams: { page: newPage }, queryParamsHandling: 'merge' });
  }
}
