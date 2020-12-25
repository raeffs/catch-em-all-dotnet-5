import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'cea-app-shell',
  templateUrl: 'app-shell.component.html',
  styleUrls: ['app-shell.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppShellComponent {}
