import { ChangeDetectorRef, Component } from '@angular/core';
import { SearchQueryService } from '@cea/domain-data-access';

@Component({
  selector: 'cea-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  term: string;
  response: unknown;

  constructor(private readonly service: SearchQueryService, private readonly changeDetectorRef: ChangeDetectorRef) {}

  send(): void {
    this.service.createSearchQuery({ searchTerm: this.term }).subscribe(result => {
      this.response = result;
      this.changeDetectorRef.detectChanges();
    });
  }
}
