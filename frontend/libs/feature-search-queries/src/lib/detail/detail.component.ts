import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'cea-search-queries-detail',
  templateUrl: 'detail.component.html',
  styleUrls: ['detail.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.ShadowDom,
})
export class DetailComponent {}
