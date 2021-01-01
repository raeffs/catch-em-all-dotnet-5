import { ChangeDetectionStrategy, ChangeDetectorRef, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  SearchQueryDetail,
  SearchQueryService,
  SearchResultService,
  SearchResultSummary,
  Sort,
} from '@cea/domain-data-access';
import { createPaginatedDataSource, PaginatedDataSource } from '@raeffs/data-source';
import { Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

@Component({
  templateUrl: 'detail.component.html',
  styleUrls: ['detail.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DetailComponent {
  public readonly columns = ['name', 'operations', 'condition', 'type', 'bidPrice', 'purchasePrice', 'ends', 'updated'];

  public readonly query: Observable<SearchQueryDetail>;
  public readonly dataSource: PaginatedDataSource<SearchResultSummary>;

  constructor(
    private readonly searchQueryService: SearchQueryService,
    private readonly searchResultService: SearchResultService,
    private readonly route: ActivatedRoute,
    private changeDetectorRef: ChangeDetectorRef
  ) {
    this.query = this.route.params.pipe(switchMap(params => this.searchQueryService.getSearchQuery(params['id'])));
    this.dataSource = createPaginatedDataSource({
      endpoint: request =>
        this.searchResultService.getAll(
          this.route.snapshot.params['id'],
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

  public openResult(query: SearchResultSummary): void {
    window.open(query.externalLink, '_blank');
  }

  public deleteResult(event: Event, item: SearchResultSummary): void {
    event.stopPropagation();
    event.preventDefault();
    this.searchResultService.delete(item.queryId, item.id).subscribe(() => this.dataSource.reload());
  }

  public handleResize(e: any): void {
    const rows = Math.floor(e.currentHeight / 45) - 2;
    this.changeDetectorRef.detectChanges();
    this.dataSource.changePageSize(rows);
  }

  public myTrackById(index: number, item: SearchResultSummary): string {
    return item.id ?? '';
  }

  public sortBy(property: keyof SearchResultSummary, current?: Sort): void {
    this.dataSource.sortBy({
      property,
      order: current?.property === property && current?.order === 'Ascending' ? 'Descending' : 'Ascending',
    });
  }
}
