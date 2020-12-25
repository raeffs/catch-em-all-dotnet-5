import { Component } from '@angular/core';
import { Service } from '@cea/data-access';

@Component({
  selector: 'cea-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  term: string;
  response: unknown;

  constructor(private readonly service: Service) {}

  send(): void {
    this.service
      .queries({ searchTerm: this.term })
      .subscribe((result) => (this.response = result));
  }
}
