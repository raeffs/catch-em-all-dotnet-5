import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  SearchQueryDetail,
  SearchQueryService,
  SearchResultService,
  SearchResultSummary,
} from '@cea/domain-data-access';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  templateUrl: 'detail.component.html',
  styleUrls: ['detail.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DetailComponent {
  public readonly query: Observable<SearchQueryDetail>;
  public readonly results: Observable<SearchResultSummary[]>;

  constructor(
    private readonly searchQueryService: SearchQueryService,
    private readonly searchResultService: SearchResultService,
    private readonly route: ActivatedRoute
  ) {
    this.query = this.route.params.pipe(switchMap(params => this.searchQueryService.getSearchQuery(params['id'])));
    this.results = this.route.params.pipe(switchMap(params => this.searchResultService.getAllResults(params['id'])));
  }

  public deleteResult(result: SearchResultSummary): void {
    this.searchResultService.deleteResult(result.queryId ?? '', result.id ?? '').subscribe();
  }
}
