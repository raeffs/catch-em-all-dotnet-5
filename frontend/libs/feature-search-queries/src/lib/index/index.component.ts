import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SearchQueryService } from '@cea/domain-data-access';

@Component({
  templateUrl: 'index.component.html',
  styleUrls: ['index.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class IndexComponent {
  public model: FormGroup;

  constructor(
    private readonly queries: SearchQueryService,
    private readonly builder: FormBuilder,
    private readonly router: Router
  ) {
    this.model = this.builder.group({
      searchTerm: ['', Validators.required],
    });
  }

  public createSearchQuery(): void {
    this.queries.createSearchQuery({ searchTerm: this.model.get('searchTerm')?.value }).subscribe(query => {
      this.router.navigate(['queries', 'detail', query.id]);
    });
  }
}
