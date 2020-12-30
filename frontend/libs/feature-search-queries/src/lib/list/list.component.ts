import { CdkTable } from '@angular/cdk/table';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { SearchQueryService, SearchQuerySummary } from '@cea/domain-data-access';
import { createPaginatedDataSource, PaginatedDataSource } from '@raeffs/data-source';

@Component({
  templateUrl: 'list.component.html',
  styleUrls: ['list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ListComponent {
  public readonly columns = ['name', 'operations', 'numberOfAuctions', 'priority', 'updated'];

  public readonly dataSource: PaginatedDataSource<SearchQuerySummary>;

  @ViewChild(CdkTable)
  private cdkTable: CdkTable<SearchQuerySummary> | null = null;

  private numberOfRows = 20;

  public get rowStyle(): string {
    return `repeat(${this.numberOfRows}, 45px)`;
  }

  constructor(
    private readonly queryService: SearchQueryService,
    private readonly router: Router,
    private changeDetectorRef: ChangeDetectorRef
  ) {
    this.dataSource = createPaginatedDataSource({
      endpoint: request => this.queryService.getAllSearchQueries(request.pageNumber, request.pageSize),
      sort: { property: 'id', order: 'asc' },
      pageNumber: 1,
      pageSize: 20,
    });
  }

  public openQuery(query: SearchQuerySummary): void {
    this.router.navigate(['queries', 'detail', query.id]);
  }

  public handleResize(e: any): void {
    console.log(e);
    const rows = Math.floor(e.currentHeight / 45) - 2;
    console.log(rows);
    this.numberOfRows = rows;
    this.changeDetectorRef.detectChanges();
    this.dataSource.changePageSize(rows);
  }

  public myTrackById(index: number, item: SearchQuerySummary): string {
    return item.id ?? '';
  }

  public deleteQuery(event: Event, item: SearchQuerySummary): void {
    event.stopPropagation();
    event.preventDefault();
    this.queryService.deleteSearchQuery(item.id).subscribe();
  }
}
