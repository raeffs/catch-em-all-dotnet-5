import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Router } from '@angular/router';
import { SearchQueryService, SearchQuerySummary } from '@cea/domain-data-access';
import { Observable } from 'rxjs';

@Component({
  templateUrl: 'list.component.html',
  styleUrls: ['list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ListComponent {
  public readonly queries: Observable<SearchQuerySummary[]>;

  constructor(private readonly queryService: SearchQueryService, private readonly router: Router) {
    this.queries = this.queryService.getAllSearchQueries();
  }

  public openQuery(query: SearchQuerySummary): void {
    this.router.navigate(['queries', 'detail', query.id]);
  }
}
