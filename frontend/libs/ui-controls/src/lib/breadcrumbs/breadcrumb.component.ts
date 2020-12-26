import { ChangeDetectionStrategy, Component, HostBinding, Optional } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'cea-breadcrumb',
  templateUrl: 'breadcrumb.component.html',
  styleUrls: ['breadcrumb.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BreadcrumbComponent {
  @HostBinding('class.is-link')
  public readonly isLink: boolean = false;

  constructor(@Optional() private readonly link: RouterLink) {
    this.isLink = !!link;
  }
}
