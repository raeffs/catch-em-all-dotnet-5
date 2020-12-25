import { Component } from '@angular/core';
import { QueryService } from '@cea/data-access';

@Component({
  selector: 'cea-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  term: string;
  response: unknown;

  constructor(private readonly service: QueryService) {}

  send(): void {
    this.service
      .createQuery({ searchTerm: this.term })
      .subscribe((result) => (this.response = result));
  }
}
