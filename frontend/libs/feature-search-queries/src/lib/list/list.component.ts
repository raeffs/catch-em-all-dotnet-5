import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  templateUrl: 'list.component.html',
  styleUrls: ['list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ListComponent {}
