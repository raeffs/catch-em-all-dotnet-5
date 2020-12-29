import { ChangeDetectionStrategy, ChangeDetectorRef, Component } from '@angular/core';
import { Router } from '@angular/router';
import { SearchQueryService, SearchQuerySummary } from '@cea/domain-data-access';
import { PaginatedDataSource } from '@cea/ui-controls';

@Component({
  templateUrl: 'list.component.html',
  styleUrls: ['list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ListComponent {
  public readonly columns = ['name', 'numberOfAuctions', 'priority', 'updated'];

  public readonly dataSource: PaginatedDataSource<SearchQuerySummary>;

  private numberOfRows = 20;

  public get rowStyle(): string {
    return `repeat(${this.numberOfRows}, 45px)`;
  }

  constructor(
    private readonly queryService: SearchQueryService,
    private readonly router: Router,
    private changeDetectorRef: ChangeDetectorRef
  ) {
    this.dataSource = new PaginatedDataSource<SearchQuerySummary>(
      request => this.queryService.getAllSearchQueries(request.page, request.size),
      { property: 'id', order: 'asc' }
    );
  }

  public openQuery(query: SearchQuerySummary): void {
    this.router.navigate(['queries', 'detail', query.id]);
  }

  public handleResize(e: any): void {
    console.log(e);
    const rows = Math.floor(e.currentHeight / 45) - 2;
    console.log(rows);
    this.numberOfRows = rows;
    this.dataSource.setPageSize(rows);
    this.changeDetectorRef.detectChanges();
  }

  public myTrackById(index: number, item: SearchQuerySummary): any {
    return item.id;
  }
}
