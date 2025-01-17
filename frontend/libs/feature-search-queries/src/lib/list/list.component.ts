import { ChangeDetectionStrategy, ChangeDetectorRef, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SearchQueryService, SearchQuerySummary, Sort } from '@cea/domain-data-access';
import { createPaginatedDataSource, PaginatedDataSource } from '@raeffs/data-source';
import { map } from 'rxjs/operators';

@Component({
  templateUrl: 'list.component.html',
  styleUrls: ['list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ListComponent {
  public readonly columns = ['name', 'operations', 'numberOfAuctions', 'priority', 'updated'];

  public readonly dataSource: PaginatedDataSource<SearchQuerySummary>;

  constructor(
    private readonly queryService: SearchQueryService,
    private readonly router: Router,
    private readonly route: ActivatedRoute,
    private changeDetectorRef: ChangeDetectorRef
  ) {
    this.dataSource = createPaginatedDataSource({
      endpoint: request =>
        this.queryService.getAllSearchQueries(
          request.pageNumber,
          request.pageSize,
          request.sort.property,
          request.sort.order
        ),
      initialSort: { property: 'name', order: 'Ascending' },
      initialPageNumber: 1,
      initialPageSize: 20,
      pageNumberChanges: this.route.queryParams.pipe(map(params => +params['page'])),
    });
  }

  public openQuery(query: SearchQuerySummary): void {
    this.router.navigate(['queries', 'detail', query.id]);
  }

  public handleResize(e: any): void {
    const rows = Math.floor(e.currentHeight / 45) - 2;
    this.changeDetectorRef.detectChanges();
    this.dataSource.changePageSize(rows);
  }

  public myTrackById(index: number, item: SearchQuerySummary): string {
    return item.id ?? '';
  }

  public deleteQuery(event: Event, item: SearchQuerySummary): void {
    event.stopPropagation();
    event.preventDefault();
    this.queryService.deleteSearchQuery(item.id).subscribe(() => this.dataSource.reload());
  }

  public sortBy(property: keyof SearchQuerySummary, current?: Sort): void {
    this.dataSource.sortBy({
      property,
      order: current?.property === property && current?.order === 'Ascending' ? 'Descending' : 'Ascending',
    });
  }
}
