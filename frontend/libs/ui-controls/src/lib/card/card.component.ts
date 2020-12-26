import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'cea-card',
  templateUrl: 'card.component.html',
  styleUrls: ['card.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CardComponent {}
