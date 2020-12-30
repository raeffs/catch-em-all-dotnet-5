import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'cea-card',
  templateUrl: 'card.component.html',
  styleUrls: ['card.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CardComponent {}
