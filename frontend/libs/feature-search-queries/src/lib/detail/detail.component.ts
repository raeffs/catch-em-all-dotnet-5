import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuctionSummary, SearchQueryDetail, SearchQueryService } from '@cea/domain-data-access';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  templateUrl: 'detail.component.html',
  styleUrls: ['detail.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DetailComponent {
  public readonly query: Observable<SearchQueryDetail>;
  public readonly auctions: Observable<AuctionSummary[]>;

  constructor(private readonly queries: SearchQueryService, private readonly route: ActivatedRoute) {
    this.query = this.route.params.pipe(switchMap(params => this.queries.getSearchQuery(params['id'])));
    this.auctions = this.route.params.pipe(switchMap(params => this.queries.getAuctions(params['id'])));
  }
}
