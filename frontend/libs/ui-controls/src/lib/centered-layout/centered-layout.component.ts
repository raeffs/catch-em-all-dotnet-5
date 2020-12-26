import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'cea-centered-layout',
  template: `<ng-content></ng-content>`,
  styles: [
    `
      :host {
        position: relative;
        height: 100%;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        margin: auto;
        text-align: center;
      }
    `,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CenteredLayoutComponent {}
