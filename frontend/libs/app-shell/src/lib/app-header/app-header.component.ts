import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'cea-app-header',
  templateUrl: 'app-header.component.html',
  styleUrls: ['app-header.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppHeaderComponent {}
