import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SearchQueryService } from '@cea/domain-data-access';

@Component({
  selector: 'cea-search-queries-index',
  templateUrl: 'index.component.html',
  styleUrls: ['index.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.ShadowDom,
})
export class IndexComponent {
  public model: FormGroup;

  constructor(private readonly queries: SearchQueryService, private readonly builder: FormBuilder) {
    this.model = this.builder.group({
      searchTerm: ['', Validators.required],
    });
  }

  public createSearchQuery(): void {
    this.queries.createSearchQuery({ searchTerm: this.model.get('searchTerm')?.value }).subscribe();
  }
}
