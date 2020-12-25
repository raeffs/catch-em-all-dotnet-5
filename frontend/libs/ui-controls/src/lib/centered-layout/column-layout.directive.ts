import { Directive, ElementRef, Renderer2 } from '@angular/core';

@Directive({
  selector: '[ceaColumnLayout]',
})
export class ColumnLayoutDirective {
  constructor(private readonly renderer: Renderer2, private readonly elementRef: ElementRef) {
    this.renderer.setStyle(this.elementRef.nativeElement, 'max-width', '600px');
  }
}
